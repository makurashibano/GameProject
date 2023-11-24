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
            if (Gamepad.current.startButton.isPressed)
            {
                canvas.SetActive(false);
                SceneManager.LoadScene("Stage");
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
