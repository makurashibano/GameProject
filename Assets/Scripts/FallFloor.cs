using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    public List<GameObject> outsideFloor = new List<GameObject>();
    public List<GameObject> insideFloor = new List<GameObject>();
    GameObject[] outsideObj;
    GameObject[] insideObj;
    public float downSpeed = 1.5f;
    public float fallInterval = 2.0f;
    public float startFallingTime = 10.0f;
    float timer = 0;
    float destroyThreshold = -5.0f;
    bool startFalling = false;
    public Material fallingObjectMaterial;
    public CountDownManager countDown;
    void Start()
    {
        outsideObj = GameObject.FindGameObjectsWithTag("OutsideFloor");
        outsideFloor.AddRange(outsideObj);
        insideObj = GameObject.FindGameObjectsWithTag("InsideFloor");
        insideFloor.AddRange(insideObj);
    }

    void Update()
    {
        if (countDown.GameStart)
        {
            if (Time.time >= startFallingTime && !startFalling)
            {
                startFalling = true;
            }
            if (startFalling)
            {
                timer += Time.deltaTime;
                if (timer >= fallInterval)
                {
                    timer = 0;
                    if (outsideFloor.Count <= 0)
                    {
                        FallRandomObject(insideFloor);
                    }
                    else
                    {
                        FallRandomObject(outsideFloor);
                    }
                }
                CheckObjectPosition(outsideFloor);
                CheckObjectPosition(insideFloor);
            }
        }
    }
    IEnumerator FallWithDelay(GameObject fallingObject)
    {
        // オブジェクトに色を付ける
        Renderer objectRenderer = fallingObject.GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectRenderer.material = fallingObjectMaterial;
        }

        // 0.5秒待機
        yield return new WaitForSeconds(0.5f);

        Rigidbody rb = fallingObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = fallingObject.AddComponent<Rigidbody>();
        }
        rb.velocity = Vector3.down * downSpeed;
    }
    void FallRandomObject(List<GameObject> floor)
    {
        if (floor.Count > 0)
        {
            int index = Random.Range(0, floor.Count);
            GameObject fallingObject = floor[index];
            StartCoroutine(FallWithDelay(fallingObject));
        }
    }
    void CheckObjectPosition(List<GameObject> objectList)
    {
        foreach (GameObject obj in objectList)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null && rb.position.y < destroyThreshold)
            {
                Destroy(obj);
                objectList.Remove(obj);
                break;
            }
        }
    }
}
