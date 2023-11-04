using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManagement : MonoBehaviour
{
    [SerializeField]
    float countdownTime = 0f;
    float CountDown = 0f;
    public TextMeshProUGUI TimerText;
    public GameObject TimeUpPanel;
    bool sceneActive = false;
    
     IEnumerator ReturnToTitle(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
        SceneManager.LoadScene("Result", LoadSceneMode.Additive);
    }
    private void Awake()
    {
        SceneManager.sceneUnloaded += ActiveSceneChanged;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (CountDown <= 1f)
        {
            return;
        }
        if (sceneActive==true)
        {
            CountDown -= Time.deltaTime;
            
            int second = (int)CountDown;
            TimerText.text = second.ToString();
            if (CountDown <= 1f)
            {

                TimeUpPanel.SetActive(true);
                second = 0;
                TimerText.text = "0";
                Time.timeScale = 0f;
                StartCoroutine(ReturnToTitle(5f));
            }
        }
    }
    
    void ActiveSceneChanged(Scene thisScene)
    {
        sceneActive = true;
        CountDown = countdownTime;
        TimeUpPanel.SetActive(false);
    }
}
