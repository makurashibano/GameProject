using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
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
