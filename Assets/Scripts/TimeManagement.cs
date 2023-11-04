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
    bool sceneActive = true;
    
    void ReturnToTitle()
    {
        SceneManager.LoadScene("title");
    }
    private void Awake()
    {
        CountDown = countdownTime;
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
                Invoke("ReturnToTitle", 5f);
            }
        }
    }
    public void StopTimer()
    {
        sceneActive = false;

    }
}
