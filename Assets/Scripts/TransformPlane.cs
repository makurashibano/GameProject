using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPlane : MonoBehaviour
{
    [SerializeField]
    float speed=1;
    [SerializeField]
    float amplitude=1.5f;
    [SerializeField]
    float startTime=5f;
    float currentTime=0f;
    float timer=0f;
    float y;
    void Start()
    {
        y = transform.position.y;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (startTime <= currentTime)
        {
            float a = Mathf.Sin(timer * speed) * amplitude;
            timer += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y + a, transform.position.z);
        }

    }
}
