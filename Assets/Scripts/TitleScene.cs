using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    float timer = 0f;
    public GameObject canvas;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (SceneManager.GetSceneByName("Title").IsValid())
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                List<InputDevice> devices = new List<InputDevice>(InputSystem.devices);

                if (devices.Count > 1)
                {
                    canvas.SetActive(false);
                    SceneManager.LoadScene("StageSelect");
                }
                else
                {
                    Debug.Log(devices.Count);
                }
            }

        }
        timer += Time.deltaTime;

        if (timer >= 10.0f)
        {
            canvas.SetActive(false);
        }
        if (timer >= 10f && timer <= 16f)
        {
            canvas.SetActive(true);
        }
        if (timer >= 16.0f)
        {
            canvas.SetActive(false);
            timer = 0f;
        }

    }
}
