using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	//playerの数を入れる配列
	GameObject[] targets;
    //playerの中で一番近いオブジェクト
    GameObject target;

    NavMeshAgent navmesh;
    Rigidbody rb;

    public float boundsPower = 10.0f;
    Vector3 forceDir = new Vector3(0f, 0f, 0f);
    bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        navmesh = GetComponent<NavMeshAgent>();
        //シーン内にあるPlayerオブジェクトを配列に入れる
        targets = GameObject.FindGameObjectsWithTag("Player");
        
    }

    //一番近いオブジェクトを探す関数
    private void  FetchNearObjectWithTag(string tagName)
    {
        // 該当タグが1つしか無い場合はそれを返す
        targets = GameObject.FindGameObjectsWithTag(tagName);
        var minTargetDistance = float.MaxValue;
        for(int i = 0; i<targets.Length;i++)
        {
            //オブジェクトが自身の場合計測しない
            if (this.gameObject == targets[i]) continue;
            // 前回計測したオブジェクトよりも近くにあれば記録
            var targetDistance = Vector3.Distance(transform.position, targets[i].transform.position);
            if (!(targetDistance < minTargetDistance)) continue;
            minTargetDistance = targetDistance;
            //一番近いオブジェクトを代入
            target = targets[i];
        }
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;
        FetchNearObjectWithTag("Player");
        //追いかけているオブジェクトとの距離
        float distance = (this.gameObject.transform.position - target.transform.position).magnitude;
        AttackFlug(distance);
        //playerをNavmeshを使って追いかける
        Debug.Log(distance);
        Debug.Log(isAttack);
        Move();
        navmesh.destination = target.transform.position;
    }

    void Move()
    {
        if (isAttack ==true)
        {
            navmesh.speed = 0f;
        }
        else if(isAttack == false)
        {
            navmesh.speed = 2f;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Push"){
            // 衝突位置を取得する
            Vector3 hitPos = other.contacts[0].point;

            // 衝突位置から自機へ向かうベクトルを求める
            Vector3 boundVec = this.transform.position - hitPos;

            // 逆方向にはねる
            forceDir = boundsPower * boundVec.normalized;
            rb.AddForce(forceDir, ForceMode.Impulse);
        }
    }
    //攻撃の判定関数
    void AttackFlug(float distance){
        if(distance < 3f){
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
    }
}
