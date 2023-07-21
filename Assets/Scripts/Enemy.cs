using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	//playerの数を入れる配列
	public GameObject[] gm;
	NavMeshAgent navmesh; 
    // Start is called before the first frame update
    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
		//playerをNavmeshを使って追いかける
        navmesh.destination = gm[0].transform.position;
    }
}
