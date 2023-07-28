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

    bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
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
		//playerをNavmeshを使って追いかける
        navmesh.destination = target.transform.position;
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
