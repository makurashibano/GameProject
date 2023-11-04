using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public GameObject Canvas;
    bool titlenow = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (titlenow)
            {
                SceneManager.UnloadScene("Title");
                titlenow = false;
            }
            if(titlenow==false)
            {
                SceneManager.UnloadScene("Result");
                Time.timeScale = 1;
                SceneManager.LoadScene("Title", LoadSceneMode.Additive);
            }
        }
    }
}
