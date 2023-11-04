using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPlane : MonoBehaviour
{
    public TimeManagement moveCountTime;
    float time = moveCountTime.countdownTime;
    float yOrigin;
    [SerializeField]
    float amplitude=0f;
    [SerializeField]
    float moveSpeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //ステージのPlaneオブジェクト上下
        yOrigin = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (time == 50)
        {
            StageUp();
            Debug.Log();

        }

    }

    void StageUp()
    {
        for (float i = 0; i < 1; i += 0.1f)
        {
            float y = Mathf.Sin(i * moveSpeed) * amplitude;
            transform.position = new Vector3(transform.position.x, y + yOrigin, transform.position.z);
            Debug.Log(i);
        }
    }
}
