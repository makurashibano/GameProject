using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resultrank : MonoBehaviour
{
    [SerializeField]
    List<GameObject> playerprefab;
    [SerializeField]
    List<Transform> playerrank;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < PlayerKillZone.rank.Count; i++)
        {
            int pl = PlayerKillZone.rank[i];
            Instantiate(playerprefab[pl], playerrank[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
