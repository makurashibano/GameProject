using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	//playerの数を入れる配列
	GameObject[] targets;
    GameObject target;
	NavMeshAgent navmesh; 
    // Start is called before the first frame update
    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        targets = GameObject.FindGameObjectsWithTag("Player");
    }

    private void  FetchNearObjectWithTag(string tagName)
    {
        // 該当タグが1つしか無い場合はそれを返す
        targets = GameObject.FindGameObjectsWithTag(tagName);
        GameObject result = null;
        var minTargetDistance = float.MaxValue;
        for(int i = 0; i<targets.Length;i++)
        {
            if (this.gameObject == targets[i]) continue;
            // 前回計測したオブジェクトよりも近くにあれば記録
            var targetDistance = Vector3.Distance(transform.position, targets[i].transform.position);
            if (!(targetDistance < minTargetDistance)) continue;
            minTargetDistance = targetDistance;
            target = targets[i];
        }
    }
    // Update is called once per frame
    void Update()
    {
        FetchNearObjectWithTag("Player");
		//playerをNavmeshを使って追いかける
        navmesh.destination = target.transform.position;
    }
}
