using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFalling : MonoBehaviour
{
    public float radius = 12;
    [SerializeField]
    List<GameObject> stonePrefabs;
    public bool isFall = false;
    float intervalTime = 5;
    public float minIntervalTime = 0f;
    public float maxIntervalTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        isFall = false;
    }

    // Update is called once per frame
    void Update()
    {   
        TimeCount();
        if(intervalTime <= 0)
        {
            var circlePos = radius * Random.insideUnitCircle;
            Instantiate(stonePrefabs[0], new Vector3(circlePos.x, transform.position.y, circlePos.y), Quaternion.identity);
            intervalTime = Random.Range(minIntervalTime, maxIntervalTime);
        }
    }
    void TimeCount()
    {
        intervalTime -= Time.deltaTime;
    }
}
