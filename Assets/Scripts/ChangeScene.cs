using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void OnGameStart()
    {
        SceneManager.LoadScene("Stage");
        SceneManager.LoadScene("GameSystem", LoadSceneMode.Additive);
    }
    void OnResultOff()
    {
        SceneManager.UnloadSceneAsync("Result");
        Time.timeScale = 1;
        SceneManager.LoadScene("title", LoadSceneMode.Additive);
    }
    
}
