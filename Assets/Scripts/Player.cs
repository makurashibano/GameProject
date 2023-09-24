using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	//ノックバックパワー
	private float boundPower = 5.0f;
	Vector3 boundVec = new Vector3(0f, 0f, 0f);


    public float speed = 0;
	public float CountDownTime = 0;
	private Vector3 playerMove;
	float rotateSpeed = 10f;
	[SerializeField]
    private float boundsPower = 12.0f;
	public bool isAttack= false;
	[SerializeField]
	GameObject[] players;
	[SerializeField]
	Vector3[] spawnPoint;
	private Vector2 moveAmount;

    // Rigidbodyコンポーネントを入れる変数"rb"を宣言する。 
    private Rigidbody rb;
	[SerializeField]
	private Collider col;
    private void Awake()
    {
        int index = GetComponent<PlayerInput>().playerIndex;
		GameObject playerSpawnPoint =GameObject.FindGameObjectWithTag("PlayerSpawnPoint" + index);
		GameObject player = Instantiate(players[index]);
		player.transform.parent = transform;
		transform.position = playerSpawnPoint.transform.position;
        transform.rotation = playerSpawnPoint.transform.rotation;
		name = "Player" + index;
    }
    void Start()
	{ 														  
		rb = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを取得する
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
	void FixedUpdate() 
	{
//		rb.velocity = new Vector3(moveAmount.x,rb.velocity.y/3.8f,moveAmount.y) * speed * Time.deltaTime;
		Vector3 force = new Vector3(moveAmount.x, rb.velocity.y / 3.8f, moveAmount.y) * speed * Time.deltaTime*5f;
		GetComponent<Rigidbody>().AddForce(force);
        //Move();
    }
    //プレイヤーの移動関数
    void Move()
    {
		//初期化
		playerMove = new Vector3(0f, 0f, 0f);
		//キーボードを取得
		var keyboard = Keyboard.current;
		//keyを取得
		var aKey = keyboard.aKey;
		var wKey = keyboard.wKey;
		var sKey = keyboard.sKey;
		var dKey = keyboard.dKey;
        var spaceKey = keyboard.spaceKey;

        if (aKey.isPressed)
		{
			playerMove.x += -0.5f;
		}
		if (dKey.isPressed)
		{
			playerMove.x += 0.5f;
		}
		if (sKey.isPressed)
		{
			playerMove.z += -0.5f;
		}
		if (wKey.isPressed)
		{
			playerMove.z += 0.5f;
		}
        
        if (spaceKey.isPressed)
        {
            
        }
        //元のスピードに戻す
        if (CountDownTime < 1.7f)
		{
			speed = 10;
		}
		//カウントが0以下なら走る
		if (CountDownTime <= 0f)
		{
			//スピードを一時的に上げる
			if (Input.GetKey(KeyCode.LeftShift))
			{
				speed = speed + 20;
				CountDownTime = 2.0f;
			}
		}
		//移動を反映
		
		//移動方向に回転している
		transform.forward = Vector3.Slerp(transform.forward, playerMove, Time.deltaTime * rotateSpeed);
	}
	//走るためのクールダウンカウント関数
	void CountDown(){
		if(CountDownTime <= 0f) CountDownTime = 0;
		CountDownTime -= Time.deltaTime;
	}
	void Update()
	{
		CountDown();
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
	
	/// <summary>
	/// 衝突発生時の処理
	/// </summary>
	/// <param name="collision">衝突したCollider</param>
	private void OnTriggerEnter(Collider collider)
	{
		///ノックバック処理

		//正規化
        Vector3 forceDir = boundsPower * transform.forward;
        //ノックバックさせる
		if (collider.transform.parent!=null)
		{
			return;
		}
//        collider.transform.GetComponent<Rigidbody>().velocity = forceDir;
        collider.transform.GetComponent<Rigidbody>().AddForce(forceDir,ForceMode.Impulse);
        ///
    }

}

