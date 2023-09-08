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

	// Rigidbodyコンポーネントを入れる変数"rb"を宣言する。 
	public Rigidbody rb; 
	void Start()
	{ 														  
		rb = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを取得する
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

	}


	/*
	//ここに
    //「もし[左クリック]を押したら」
    //「[オブジェクト：Punch]を[z +0.5]の座標に出す」
	//ってやつを書く
	//ちなみに[オブジェクト：Punch]はコライダーが isTrigger になってて今のところ当たり判定はない

	//あとまあさっきの吹き飛ばしも「自分が [オブジェクト：Punch] に触れたら」っていうif文に変える

	
		if(collision.gameObject.tag == "Attack")
	
	


	*/


	//ノックバックするコルーチン
	IEnumerator Knock()
    {
		//ノックバックに力を加える回数
		int count = 0;
		while(true)
        {
			count++;
			//ノックバックを行っている
			rb.AddForce(boundVec * boundPower, ForceMode.Impulse);
			yield return null;
			//100回力を加えるとコルーチンを抜け出す
			if(count == 100)
            {
				yield break; 
            }
        }
    }
	

	/// <summary>
	/// 衝突発生時の処理
	/// </summary>
	/// <param name="collision">衝突したCollider</param>
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Finish")
        {
			// 衝突位置を取得する
			Vector3 hitPos = collision.contacts[0].point;

			// 衝突位置から自機へ向かうベクトルを求める
			boundVec = this.transform.position - hitPos;
			boundVec.y = 0;

			StartCoroutine("Knock");
		}
	}
	
}

