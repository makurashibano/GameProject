using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StertButtonScript : MonoBehaviour
{
    public string sceneName;
    public float count = 2.0f;
    public void OnClickStartButton()
    {
        Invoke("game", count);
    }
    void game()
    {
        SceneManager.LoadScene(sceneName);
    }
}
