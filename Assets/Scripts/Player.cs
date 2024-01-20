using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
	public static List<int> rank = new List<int>();
	public static int totalPlayersCount;
	[SerializeField]
	TMPro.TMP_Text playerText;
	[SerializeField]
	GameObject canvas;
	[SerializeField]
	List<Color> playerColor;
	[SerializeField]
	List<Color> playerColor1;
	public int PlayerIndex
    {
		get { return GetComponent<PlayerInput>().playerIndex; }
    }
	LayerMask otherLayerMaskPlayer;

	[SerializeField]
	Animator animator;

	//速さ
	public float speed = 0;
	//ダッシュの速さ
	[SerializeField]
	private float dashSpeed = 0f;
	//ダッシュ判定
	private bool isdash = false;
	//ダッシュ後のクールタイム
	private float coolTime = 0;
	private bool iscoolTime = false;

    public float UnControllableTimer = 0.0f;

	//押し出す力
	[SerializeField]
    private float boundsPower = 12.0f;
	[SerializeField]
	private float bodyBoundsPower = 4;
	
	//攻撃ゲージ
	private int attackCount = 0;
	[SerializeField]
	private int maxAttackCount = 4;
	private bool isSpecialAttack = false;
	[SerializeField]
	private float specialAttackMultiplier = 2.0f;

	//攻撃
	public bool isAttack= false;
	//攻撃クールタイム
	bool isAttackCoolTime = false;

	//ダメージ
	public bool isDamage = false;
	float damageCount = 0f;
	//スタン
	public bool isStan = false;
	[SerializeField]
	GameObject[] players;
	//出現地点
	[SerializeField]
	Vector3[] spawnPoint;
	private Vector2 moveAmount;

	//サウンド
	AudioSource audioSource;
	public AudioClip attack_SE;
	public AudioClip damage_SE;

	// Rigidbodyコンポーネントを入れる変数"rb"を宣言する。 
	private Rigidbody rigidbody;
	//攻撃コライダー
	[SerializeField]
	private Collider col;
	//タイムアップパネル
	public GameObject obj;

	public GameObject timeManagement;

	public GameObject Managers;

	[SerializeField]
	public List<GameObject> playerParticles;
	//子オブジェクトを格納
	public List<GameObject> childObjects;

	//プレイヤー番号を表示する時間
	float inactiveTimer = 0f;
	private void Awake()
    {
		otherLayerMaskPlayer = LayerMask.NameToLayer("Player");
		obj = GameObject.Find("Canvas").transform.Find("TimeUpPanel").gameObject;
		int index = GetComponent<PlayerInput>().playerIndex;
		GameObject playerSpawnPoint =GameObject.FindGameObjectWithTag("PlayerSpawnPoint" + index);

		//頭上にプレイヤーのナンバーを表示
		PlayerUI(index);

		//キャラクターのモデルを表示
		GameObject player = Instantiate(players[index],new Vector3(transform.position.x, transform.position.y-0.5f, transform.position.z), transform.rotation);
		animator = player.GetComponent<Animator>();
		player.transform.parent = transform;

		//キャラクターのスポーンポイント
		transform.position = playerSpawnPoint.transform.position;
        transform.rotation = playerSpawnPoint.transform.rotation;
		name = "Player" + index;

		//ランキングを初期化
		rank.Clear();

		GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");
		totalPlayersCount = currentPlayers.Length;

		timeManagement = GameObject.FindGameObjectWithTag("TimeManagement") ?? timeManagement;
		timeManagement.SetActive(true);
		canvas.SetActive(true);
		GetChildren(player);
	}
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		rigidbody = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを取得する
		col.enabled = false;
		isDamage = false;
		isAttackCoolTime = false;
		Managers = GameObject.Find("Managers");
	}
	//子を取得
	void GetChildren(GameObject playerModel)
	{
		Transform children = playerModel.GetComponentInChildren<Transform>();
		//子要素がいなければ終了
		if (children.childCount == 0)
		{
			return;
		}
		//modelのBodyだけ取得
		foreach (Transform child in children)
		{
			if (child.gameObject.tag == "ModelBody")
			{
				childObjects.Add(child.gameObject);
			}
			GetChildren(child.gameObject);
		}
	}
	//プレイヤーの番号を表示
	void PlayerUI(int playerindex)
    {
		playerText.text = $"P{playerindex + 1}";
		TMPro.VertexGradient vertexGradient = new TMPro.VertexGradient(playerColor[playerindex]);

		vertexGradient.topLeft = playerColor[playerindex];
		vertexGradient.topRight = playerColor[playerindex];
		vertexGradient.bottomLeft = playerColor1[playerindex];
		vertexGradient.bottomRight = playerColor1[playerindex];

		playerText.colorGradient = vertexGradient;
	}

	void OnMove(InputValue value)
	{
		moveAmount = value.Get<Vector2>();
    } 
	void OnAttack()
	{

		//スタートまで硬直
		if (Managers.GetComponent<CountDownManager>().GameStart == false) return;
		if (isDamage) return;
		if (isAttackCoolTime) return;

		isAttack = true;
		isAttackCoolTime = true;
		//攻撃クールタイム
		Invoke("AttackCoolTime", 0.7f);

		//コライダーのオンオフ
		Invoke("AttackFalse", 0.3f);
		audioSource.PlayOneShot(attack_SE);
		animator?.SetTrigger("IsAttack");

	}

	void OnDash()
	{
		isdash = true;
	}
	void FixedUpdate() 
	{
		//スタートまで硬直
		if (Managers.GetComponent<CountDownManager>().GameStart == false) return;

		//時間切れになったら動けないようにする
		if (Managers.GetComponent<TimeManagement>().isdrawStopTime == true) return;
		//スタン判定
        if (isStan)
        {
			Invoke("StanTime", 0.5f);
			return;
        }
		//ダメージの動かせない時間
		if (UnControllableTimer > 0f)
		{
            UnControllableTimer -= Time.deltaTime;
        }
        if (UnControllableTimer > 0f)
		{
			return;
		}
        
		Vector2 moveAmountNormalized = moveAmount.normalized;
		Vector3 force = new Vector3(moveAmountNormalized.x, 0f, moveAmountNormalized.y) * speed + (Vector3.up* rigidbody.velocity.y);

        //クールタイムがfalse ダッシュがtrueの時走る
        if (isdash == true && iscoolTime == false)
		{		
            force = new Vector3(moveAmountNormalized.x, 0f, moveAmountNormalized.y) * dashSpeed + (Vector3.up * rigidbody.velocity.y);
        }
		//移動
		rigidbody.velocity = force;
		//移動量が一定速度にいかないと回転しない
		if (Mathf.Abs(moveAmount.x) <0.1f && Mathf.Abs(moveAmount.y )< 0.1f)
		{
			return;
		}
		//プレイヤーの向きを動かす方向に回転
		Quaternion rotation = Quaternion.LookRotation(force);
		transform.rotation = rotation;
		    
    }

	
	void Update()
	{
		//プレイヤーの番号を時間が来たら非表示にする
		inactiveTimer += Time.deltaTime;
		if (inactiveTimer >= 7.0f)
		{
			canvas.SetActive(false);
		}

		//スタートまで硬直
		if (Managers.GetComponent<CountDownManager>().GameStart == false) return;
		//ダメージ
		if (isDamage)
        {
			DamageFalse();
			StartCoroutine(Hit(0.2f));
		}
		//ダッシュ中のクールタイム
		if (isdash)
		{
			DashCoolTimeCount();
		}
		//時間切れになったら動けないようにする
		if (Managers.GetComponent<TimeManagement>().isdrawStopTime == true)
        {
			rigidbody.useGravity = false;
			return;
		}
		//スタン判定
		if (isStan)
		{
			StartCoroutine(Hit(0.6f));
			Invoke("StanTime", 0.5f);
			return;
		}
		//攻撃する
		if (isAttack)
        {
            col.enabled = true;
		}
		else
		{
			col.enabled = false;
		}


    }
	void StanTime()
    {
		isStan = false;
    }
	//走るためのクールタイムカウント関数
	void DashCoolTimeCount()
	{
		coolTime += Time.deltaTime;
		//ダッシュ時間
		if (coolTime >= 0.5f)
		{
			iscoolTime = true;
		}
		//次ダッシュできるまでのカウント
		if (coolTime >= 6f)
		{
			coolTime = 0;
			iscoolTime = false;
			isdash = false;
		}
	}
	void AttackFalse()
	{
		isAttack = false;
	}
	void AttackCoolTime()
    {
		isAttackCoolTime = false;
    }
	void DamageFalse()
	{

		//ダメージ処理
		damageCount += Time.deltaTime;
		if (damageCount > 0.3f)
		{
			isDamage = false;
			damageCount = 0f;
		}
	}
	bool hasCollided = false;
	private void OnTriggerEnter(Collider collider)
    { //Playerとステージ
		if (collider.tag == "PlayerKillZone")
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			if (PlayerIndex >= 0)
            {
				rank.Insert(0, PlayerIndex);
			}
			if (players.Length == 2)
			{
				for (int i = 0; i < totalPlayersCount; i++)
				{
					if (rank.IndexOf(i) == -1)
					{
						rank.Insert(0, i);
						break;
					}
				}
				if (!SceneManager.GetSceneByName("Result").IsValid())
				{

					players = GameObject.FindGameObjectsWithTag("Player");
					RoadScene();
					foreach (var g in players)
					{
						Destroy(g);
					}
					return;
				}
			}
			Destroy(gameObject);
			return;
		}
		
		///ノックバック処理		
        if (collider.gameObject.layer == otherLayerMaskPlayer)
		{
			//攻撃ゲージ
			if (!hasCollided)
			{
				attackCount++;
				hasCollided = true;
				Debug.Log(attackCount);
				if (attackCount >= maxAttackCount)
				{
					isSpecialAttack = true;
					attackCount = 0;
				}

			}
			//攻撃ゲージ
			float knockbackMultiplier = isSpecialAttack ? specialAttackMultiplier : 1.0f;
			//敵と自分との距離
			float playerRange = Vector3.Distance(collider.gameObject.transform.position, this.gameObject.transform.position);
			Debug.Log(playerRange);
			//正規化
			Vector3 forceDir = boundsPower * transform.forward;

			collider.GetComponent<Player>().UnControllableTimer = 0.5f;
			//攻撃されたときに攻撃できないようにする
			collider.GetComponent<Player>().isDamage = true;
            //SE
            collider.GetComponent<AudioSource>().PlayOneShot(collider.GetComponent<Player>().damage_SE,0.1f);
			//パーティクル表示
			GameObject particle = Instantiate(playerParticles[1], collider.transform.position, Quaternion.identity);
			Destroy(particle, 0.8f);
			//近さで飛ぶ力を変える
			if (playerRange <= 1.4f)
			{
				forceDir = forceDir * 1.5f;
			}
			else if (playerRange <= 1.8f)
			{
				forceDir = forceDir * 1.3f;
			}
			else if (playerRange <= 2.1f)
            {
				forceDir = forceDir * 1.1f;
			}
			else if (playerRange <= 2.3f)
			{
				forceDir = forceDir * 1.0f;
			}
			else
            {
				forceDir = forceDir * 0.7f;
			}
			//敵を飛ばす
			collider.transform.GetComponent<Rigidbody>().velocity = forceDir*knockbackMultiplier;
			isSpecialAttack = false;
		}
	}

    private void OnTriggerExit(Collider other)
    {
		if (other.gameObject.layer == otherLayerMaskPlayer)
		{
			hasCollided = false;
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
		///ノックバック処理		
		if (collision.gameObject.layer == otherLayerMaskPlayer)
		{
			//正規化
			Vector3 forceDir = bodyBoundsPower * (this.gameObject.transform.position- collision.transform.position);
			//ノックバックさせる
			collision.gameObject.GetComponent<Player>().UnControllableTimer = 0.5f;
			//攻撃されたときに攻撃できないようにする
			collision.gameObject.GetComponent<Player>().isDamage = true;
			//SE
			collision.gameObject.GetComponent<AudioSource>().PlayOneShot(collision.gameObject.GetComponent<Player>().damage_SE, 0.1f);
            //パーティクル表示
            GameObject particle = Instantiate(playerParticles[1], collision.gameObject.transform.position, Quaternion.identity);
			Destroy(particle, 0.8f);
			//敵を飛ばす
			gameObject.transform.GetComponent<Rigidbody>().velocity = forceDir;
		}
	}

	//攻撃されたときとお互いに衝突した時にダメージのフラッシュを入れる
    private IEnumerator Hit(float time)
	{
		//体を白くする（引数にalphaの値を入れる）
		BodyFlash(160);
		// 0.2秒間待つ
		yield return new WaitForSeconds(time);
		BodyFlash(0);
	}

	//ダメージ受けた時光らせる
	void BodyFlash(byte alpha)
	{
		foreach (GameObject obj in childObjects)
		{
			MeshRenderer meshRenderer;
			meshRenderer = obj.GetComponent<MeshRenderer>();
			meshRenderer.materials[1].color = new Color32(255, 255, 255, alpha);
		}
	}
	void RoadScene()
	{
		timeManagement.SetActive(false);
		SceneManager.LoadScene("Result");
	}
}

