using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
	public static List<int> rank = new List<int>();
	public static int totalPlayersCount; 

	public int PlayerIndex
    {
		get { return GetComponent<PlayerInput>().playerIndex; }
    }
   
	//ノックバックパワー
	private float boundPower = 5.0f;
	Vector3 boundVec = new Vector3(0f, 0f, 0f);

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
	//攻撃
	public bool isAttack= false;
	[SerializeField]
	GameObject[] players;
	//出現地点
	[SerializeField]
	Vector3[] spawnPoint;
	private Vector2 moveAmount;

	//初期化用変数
	private Vector3 PlayerPositionInitialization;


	// Rigidbodyコンポーネントを入れる変数"rb"を宣言する。 
	private Rigidbody rigidbody;
	[SerializeField]
	private Collider col;

	private GameObject timeManagement;

	private void Awake()
    {
        int index = GetComponent<PlayerInput>().playerIndex;
		GameObject playerSpawnPoint =GameObject.FindGameObjectWithTag("PlayerSpawnPoint" + index);
		GameObject player = Instantiate(players[index],new Vector3(transform.position.x, transform.position.y-0.5f, transform.position.z), transform.rotation);
		player.transform.parent = transform;
		transform.position = playerSpawnPoint.transform.position;
        transform.rotation = playerSpawnPoint.transform.rotation;
		name = "Player" + index;
		PlayerPositionInitialization = this.gameObject.transform.position;
		rank.Clear();
		GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");
		totalPlayersCount = currentPlayers.Length;
		timeManagement = GameObject.FindGameObjectWithTag("TimeManagement") ?? timeManagement;
		timeManagement.SetActive(true);
	}
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを取得する
		col.enabled = false;

	}
	void OnMove(InputValue value)
	{
		moveAmount = value.Get<Vector2>();
    } 
	void OnAttack()
	{
        isAttack = true;
        Invoke("AttackFalse", 0.5f);
    }

	void OnDash()
	{
		isdash = true;
	}
	void FixedUpdate() 
	{
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
            rigidbody.velocity = new Vector3(moveAmountNormalized.x, 0f, moveAmountNormalized.y) * dashSpeed + (Vector3.up * rigidbody.velocity.y);
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
		if (coolTime >= 2f)
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

    }
	void AttackFalse()
	{
		isAttack = false;
	}

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
					SceneManager.LoadScene("Result", LoadSceneMode.Additive);
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
		LayerMask otherLayerMaskPlayer = LayerMask.NameToLayer("Player");
        if (collider.gameObject.layer == otherLayerMaskPlayer)
		{
            //正規化
            Vector3 forceDir = boundsPower * transform.forward;
            //ノックバックさせる
            //        collider.transform.GetComponent<Rigidbody>().velocity = forceDir;
            collider.GetComponent<Player>().UnControllableTimer = 0.5f;
            collider.transform.GetComponent<Rigidbody>().velocity = forceDir;
            ///

        }
    }

}

