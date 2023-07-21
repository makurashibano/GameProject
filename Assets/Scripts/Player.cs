using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0;


    // Rigidbodyコンポーネントを入れる変数"rb"を宣言する。 
	public Rigidbody rb; 
	void Start() { 
		// Rigidbodyコンポーネントを取得する 
		rb = GetComponent<Rigidbody>(); 
	}
	void FixedUpdate() { 
		//  Upキーで上昇 
		if (Input.GetKey("W")) { 
			rb.velocity = new Vector3(0, 0, speed); 
		} 
		// Downキーで下降 
		if (Input.GetKey("S")) { 
			rb.velocity = new Vector3(0, 0, -speed); 
		} 
		// right キーで右に進む 
		if (Input.GetKey("A")) { 
			rb.velocity = new Vector3(speed, 0, 0); 
		} 
		//left キーで左に進む 
		if (Input.GetKey("D")) { 
			rb.velocity = new Vector3(-speed, 0, 0); 
		} 
	}
}

