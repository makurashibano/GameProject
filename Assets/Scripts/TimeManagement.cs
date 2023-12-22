using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManagement : MonoBehaviour
{
    [SerializeField]
    float countdownTime = 60f;
    float CountDown = 0f;
    public TextMeshProUGUI TimerText;
    public GameObject TimeUpPanel;
    [SerializeField]
    CountDownManager countDownManager;

// <<<<<<< HEAD

    bool sceneActive = true;

// =======
    
    public bool isdrawStopTime = false;
//  >>>>>>> main

    void ReturnToTitle()
    {
        SceneManager.LoadScene("title");
    }
    private void Awake()
    {
        CountDown = countdownTime;
        TimerText.text = " ";
    }
    
    // Update is called once per frame
    void Update()
    {
        // CountDown��1�ȉ��ɂȂ��return���Ăǂ��ɍs���񂾁H
        if (CountDown <= 1f)
        {
            return;
        }

        if (sceneActive==true)
        {
            if (countDownManager.GameStart == false) return;

            int second = (int)CountDown;
            TimerText.text = second.ToString();

            CountDown -= Time.deltaTime;
          

            if (CountDown <= 0f)
            {
                TimeUpPanel.SetActive(true);
                isdrawStopTime = true;
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
