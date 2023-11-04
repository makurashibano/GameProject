using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public GameObject Canvas;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (SceneManager.GetSceneByName("Title").IsValid())
            {
                SceneManager.UnloadScene("Title");
            }
            if (SceneManager.GetSceneByName("Result").IsValid())
            {
                SceneManager.UnloadScene("Result");
                Time.timeScale = 1;
                SceneManager.LoadScene("Title", LoadSceneMode.Additive);
            }
        }
    }
}
