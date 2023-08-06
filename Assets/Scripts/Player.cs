using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0;
	public float CountDownTime = 0;

    // Rigidbodyコンポーネントを入れる変数"rb"を宣言する。 
	public Rigidbody rb; 
	void Start() { 
		// Rigidbodyコンポーネントを取得する 
		rb = GetComponent<Rigidbody>(); 
	}
	void FixedUpdate() { 
		

		if(CountDownTime  < 1.7f){
			speed = 10;
		}

		rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed , rb.velocity.y, Input.GetAxis("Vertical") * speed);
		if (CountDownTime <= 0f){
			if(Input.GetKey(KeyCode.LeftShift)){
				speed = speed + 20;
				Debug.Log(speed);
				CountDownTime = 2.0f;
			}
		}		
	}

	void CountDown(){
		if(CountDownTime <= 0f) CountDownTime = 0;
		Debug.Log(CountDownTime);
		CountDownTime -= Time.deltaTime;
	}

	private Vector3 latestPos;  //前回のPosition

	void Update(){
		CountDown();

		Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
		latestPos = transform.position;  //前回のPositionの更新
		latestPos.x = 0f;
		latestPos.z = 0f;

		//ベクトルの大きさが0.01以上の時に向きを変える処理をする
		if (diff.magnitude > 0.01f)
		{
			transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
		}
	}

}

