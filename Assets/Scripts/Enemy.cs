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

    Vector3 force = new Vector3 (0.0f,0.0f,0.5f);
    bool isDamage = false;
    bool isAttack = false;
    [SerializeField]
    float LimitSpeed;
    float kinematicCount = 0.0f;
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
        FetchNearObjectWithTag("Player");
        //追いかけているオブジェクトとの距離
        float distance = (this.gameObject.transform.position - target.transform.position).magnitude;
        AttackFlug(distance);
        if(isDamage){
            rb.isKinematic = false;
            rb.AddForce(force, ForceMode.Impulse);
            //速度を制限
            if (rb.velocity.magnitude > LimitSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x / 1.1f, rb.velocity.y, rb.velocity.z / 1.1f);
            }          
            KinematicCount();
        }
        //playerをNavmeshを使って追いかける
        navmesh.destination = target.transform.position;    
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            isDamage = true;
        }
    }

    void KinematicCount(){
        kinematicCount += 1f * Time.deltaTime;
        if(kinematicCount>0.5f){
            rb.isKinematic = true;
            isDamage = false;
            kinematicCount = 0f;
        }
    }
    //攻撃の判定関数
    void AttackFlug(float distance){
        if(distance < 2){
            isAttack = true;
        }
        else{
            isAttack = false;
        }
    }
}
