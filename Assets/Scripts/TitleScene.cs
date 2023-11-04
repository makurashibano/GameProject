using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public GameObject Canvas;
    void OnGameStart()
    {
        SceneManager.UnloadScene("Title");
    }
    void OnResultOff()
    {
        SceneManager.UnloadScene("Result");
        Time.timeScale = 1;
        SceneManager.LoadScene("Title", LoadSceneMode.Additive);
        
    }
}
