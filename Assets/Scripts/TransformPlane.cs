using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPlane : MonoBehaviour
{
    float y;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        //ステージのPlaneオブジェクト上下
        pos = transform.position;
        y = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (y >= 2 || y<0)
        {
            y += -1f * Time.deltaTime;
        }
        else if(y <= -2 || y > 0)
        {
            y += 1f * Time.deltaTime;
        }
        

        transform.position = new Vector3(0, y+1.5f, 0);
    }
}
