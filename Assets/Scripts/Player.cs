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
	//ノックバックパワー
	private float boundPower = 5.0f;
	Vector3 boundVec = new Vector3(0f, 0f, 0f);

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

	float rotateSpeed = 10f;
	//押し出す力
	[SerializeField]
    private float boundsPower = 12.0f;

	//攻撃ゲージ
	private int attackCount = 0;
	[SerializeField]
	private int maxAttackCount = 4;
	private bool isSpecialAttack = false;
	[SerializeField]
	private float specialAttackMultiplier = 2.0f;

	//攻撃
	public bool isAttack= false;
	//ダメージ
	public bool isDamage = false;
	float damageCount = 0f;

	[SerializeField]
	GameObject[] players;
	//出現地点
	[SerializeField]
	Vector3[] spawnPoint;
	private Vector2 moveAmount;

	//初期化用変数
	private Vector3 PlayerPositionInitialization;

	AudioSource audioSource;
	public AudioClip attack_SE;
	public AudioClip damage_SE;

	// Rigidbodyコンポーネントを入れる変数"rb"を宣言する。 
	private Rigidbody rigidbody;
	[SerializeField]
	private Collider col;

	public GameObject obj;

	public GameObject timeManagement;
	private GameObject BGMObject;

	public GameObject Managers;

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
		PlayerPositionInitialization = this.gameObject.transform.position;

		//ランキングを初期化
		rank.Clear();


		GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");
		totalPlayersCount = currentPlayers.Length;

		timeManagement = GameObject.FindGameObjectWithTag("TimeManagement") ?? timeManagement;
		timeManagement.SetActive(true);
		canvas.SetActive(true);
	}
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		rigidbody = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを取得する
		col.enabled = false;
		isDamage = false;
		Managers = GameObject.Find("Managers");
	}

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
		if (isDamage == true) return;

		isAttack = true;
		Invoke("AttackFalse", 0.5f);
		audioSource.PlayOneShot(attack_SE);
		animator?.SetTrigger("IsAttack");

	}

	void OnDash()
	{
		isdash = true;
	}
	void FixedUpdate() 
	{
		if(Managers.GetComponent<TimeManagement>().isdrawStopTime == true) return;
		if (UnControllableTimer > 0f)
		{
            UnControllableTimer -= Time.deltaTime;
        }

        if (UnControllableTimer > 0f)
		{
			return;
		}
        //rb.velocity = new Vector3(moveAmount.x,rb.velocity.y/3.8f,moveAmount.y) * speed * Time.deltaTime;
		Vector2 moveAmountNormalized = moveAmount.normalized;
		Vector3 force = new Vector3(moveAmountNormalized.x, 0f, moveAmountNormalized.y) * speed + (Vector3.up* rigidbody.velocity.y);

        //クールタイムがfalse ダッシュがtrueの時走る
        if (isdash == true && iscoolTime == false)
		{		
            force = new Vector3(moveAmountNormalized.x, 0f, moveAmountNormalized.y) * dashSpeed + (Vector3.up * rigidbody.velocity.y);
        }

		rigidbody.velocity = force;

		if (Mathf.Abs(moveAmount.x) <0.1f && Mathf.Abs(moveAmount.y )< 0.1f)
		{
			return;
		}
        Quaternion rotation = Quaternion.LookRotation(force);
		transform.rotation = rotation;
		    
    }


	void Initialization()
    {
		isdash = false;
		iscoolTime = false;
		isAttack = false;
		this.gameObject.transform.position = PlayerPositionInitialization;
    }

	//走るためのクールタイムカウント関数
	void CoolTimeCount()
	{
        coolTime += Time.deltaTime;
		if (coolTime >= 0.5f)
		{
            iscoolTime = true;
		}
        if (coolTime >= 6f)
		{
			coolTime = 0;
			iscoolTime = false;
            isdash = false;
        }
	}
	void Update()
	{
		//ダメージ
		if (isDamage)
        {
			DamageFalse();
        }
		if (isdash)
		{
			CoolTimeCount();
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
		inactiveTimer += Time.deltaTime;
        if (inactiveTimer >= 3.0f)
        {
			canvas.SetActive(false);
        }

    }
	void AttackFalse()
	{
		isAttack = false;
	}
	void DamageFalse()
	{
		
		damageCount += Time.deltaTime;
		//Debug.Log(damageCount);
		if(damageCount>0.3f)
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

					timeManagement.SetActive(false);
					BGMObject = GameObject.FindGameObjectWithTag("BGM");
					Destroy(BGMObject);
					SceneManager.LoadScene("Result");
					players = GameObject.FindGameObjectsWithTag("Player");
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
				Debug.Log(attackCount + "回");
				if (attackCount >= maxAttackCount)
				{
					isSpecialAttack = true;
					attackCount = 0;
				}

			}
			//攻撃ゲージ
			float knockbackMultiplier = isSpecialAttack ? specialAttackMultiplier : 1.0f;

			//正規化
			Vector3 forceDir = boundsPower * transform.forward;
			//ノックバックさせる
			//        collider.transform.GetComponent<Rigidbody>().velocity = forceDir;
			collider.GetComponent<Player>().UnControllableTimer = 0.5f;
			//攻撃されたときに攻撃できないようにする
			collider.GetComponent<Player>().isDamage = true;
			collider.GetComponent<AudioSource>().PlayOneShot(collider.GetComponent<Player>().damage_SE,0.1f);
            collider.transform.GetComponent<Rigidbody>().velocity = forceDir*knockbackMultiplier;
			Debug.Log((forceDir * knockbackMultiplier).magnitude);
		}
	}
    private void OnTriggerExit(Collider other)
    {
		if (other.gameObject.layer == otherLayerMaskPlayer)
		{
			hasCollided = false;
		}
	}
    
}

