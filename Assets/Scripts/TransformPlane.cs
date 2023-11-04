using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPlane : MonoBehaviour
{
    float yOrigin;
    [SerializeField]
    float amplitude=1.5f;
    [SerializeField]
    float moveSpeed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        //ステージのPlaneオブジェクト上下
        yOrigin = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (y >= 2 || y < 0)
        {
            y += -1f * Time.deltaTime;
        }
        else if(y <= -2 || y > 0)
        {
            y += 1f * Time.deltaTime;
        }
        

        transform.position = new Vector3(0, y+1.5f, 0);
        */
        float y=Mathf.Sin(Time.time*moveSpeed)*amplitude;
        transform.position = new Vector3(transform.position.x, y+yOrigin,transform.position.z);
    }
}
