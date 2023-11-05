using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public GameObject Canvas;
    private void Update()
    {
        if (SceneManager.GetSceneByName("Title").IsValid())
        {
            if (Gamepad.current.buttonSouth.isPressed)
            {
                SceneManager.LoadScene("Stage");
            }
        }
        //if (SceneManager.GetSceneByName("Result").IsValid())
        //{
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        SceneManager.UnloadScene("Result");
        //        Time.timeScale = 1;
        //        SceneManager.LoadScene("Title", LoadSceneMode.Additive);
        //    }
        //}
    }
}
