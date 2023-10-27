using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
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
//		rb.velocity = new Vector3(moveAmount.x,rb.velocity.y/3.8f,moveAmount.y) * speed * Time.deltaTime;
		Vector2 moveAmountNormalized = moveAmount.normalized;
		Vector3 force = new Vector3(moveAmountNormalized.x, 0f, moveAmountNormalized.y) * speed + (Vector3.up* rigidbody.velocity.y);

		rigidbody.velocity = force;
        //クールタイムがfalse ダッシュがtrueの時走る
        if (isdash == true && iscoolTime == false)
		{		
            rigidbody.velocity = new Vector3(moveAmountNormalized.x, 0f, moveAmountNormalized.y) * dashSpeed + (Vector3.up * rigidbody.velocity.y);
        }

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
		Debug.Log(coolTime);
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
	{
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

