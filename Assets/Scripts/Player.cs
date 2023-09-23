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

    // Rigidbodyコンポーネントを入れる変数"rb"を宣言する。 
    private Rigidbody rb;
	[SerializeField]
	private Collider col;

	void Start()
	{ 														  
		rb = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを取得する
		col.enabled = false; 
	}
	void FixedUpdate() 
	{
		Move();
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
            isAttack = true;
            Invoke("AttackFalse", 0.5f);
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
		rb.velocity = new Vector3(playerMove.x * speed, rb.velocity.y, playerMove.z * speed);
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
		//当たった場所を取得
		Vector3 hitPos = collider.ClosestPointOnBounds(this.transform.position);
		Debug.Log(hitPos);
		//敵の場所を取得
        Vector3 boundVec = this.transform.position - hitPos;
		
		boundVec.y = 0f;
		//正規化
        Vector3 forceDir = boundsPower * boundVec.normalized;
        //ノックバックさせる
        this.GetComponent<Rigidbody>().velocity = forceDir;
        ///
    }
	
}

