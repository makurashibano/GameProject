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
    int moveCount = 0;

    bool isMove = true;

    private CountDownManager countManager;

    void Start()
    {
        moveCount = 0;
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
            float move2 = Mathf.Abs(move);

            if (moveCount == 0)
            {
                transform.position = new Vector3(transform.position.x, y + move2, transform.position.z);
                if (move2 > 1.2f) moveCount++;
            }
            else
            {
                if (move2 < 1.3f && move2 > 0.35f)
                {
                    transform.position = new Vector3(transform.position.x, y + move2, transform.position.z);
                }
            }
            
            
        }
    }
}
