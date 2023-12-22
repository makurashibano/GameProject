using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPlane : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;
    [SerializeField]
    float amplitude = 1.5f;
    [SerializeField]
    float startTime = 5f;

    float currentTime = 0f;
    float timer = 0f;
    float y;
    float stopTimer = 0f;

    bool isMove = true;

    private CountDownManager countManager;

    void Start()
    {
        y = transform.position.y;
        countManager = GameObject.Find("Managers").GetComponent<CountDownManager>();
    }
    void Update()
    {
        if (countManager.GameStart == false) return;
        currentTime += Time.deltaTime;

        if (startTime <= currentTime)
        {
            float move = Mathf.Sin(timer * speed) * amplitude;
            timer += Time.deltaTime;
            if (isMove)
            {
                transform.position = new Vector3(transform.position.x, y + move, transform.position.z);
                if (transform.position.y >= 0.49f)
                {
                    isMove = false;
                    return;
                }
            }
            if (!isMove)
            {
                stopTimer += Time.deltaTime;
                if (stopTimer >=2f)
                {
                    stopTimer = 0f;
                    isMove = true;
                }
                return;
            }
        }
    }
}
