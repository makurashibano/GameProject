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
    public float UnControllableTimer = 0.0f;

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
		GameObject player = Instantiate(players[index], transform.position, transform.rotation);
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
		if (UnControllableTimer > 0f)
		{
            UnControllableTimer -= Time.deltaTime;
        }
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (UnControllableTimer > 0f)
		{
			return;
		}
//		rb.velocity = new Vector3(moveAmount.x,rb.velocity.y/3.8f,moveAmount.y) * speed * Time.deltaTime;
		Vector2 moveAmountNormalized = moveAmount.normalized;
		Vector3 force = new Vector3(moveAmountNormalized.x, rb.velocity.y / 3.8f, moveAmountNormalized.y) * speed * Time.deltaTime;

		GetComponent<Rigidbody>().velocity = force;
		if(Mathf.Abs(moveAmount.x) <0.1f && Mathf.Abs(moveAmount.y )< 0.1f)
		{
			return;
		}
        Quaternion rotation = Quaternion.LookRotation(force);
		transform.rotation = rotation;
        //Move();
    }
    //プレイヤーの移動関数
    
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

