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
    float StartCount = 5f;
    public TextMeshProUGUI TimerText;
    public GameObject TimeUpPanel;

// <<<<<<< HEAD

    bool sceneActive = false;
    bool GameStart = true;

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
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameStart == true)
        {
            

            StartCount -= Time.deltaTime;
            // 試合時間のやつもカウントダウンだから、5秒間だけ数を数えて下のコードに渡すみたいな処理を作りたい
            // たぶんそうすればこっちの処理を先にするから、結果的に誰も動かない時間になるんじゃないのか？
            // プレイヤーキャラが生成されるのはどのタイミング？
            
            int second = (int)StartCount;


            // あとはここにカウントダウンを５秒から始めるやつがあれば完璧


            TimerText.text = second.ToString();

            if(StartCount <= 0)
            {
                TimerText.text = "start!";
                // こうやったら３秒だけ表示して開始するんじゃね？

                if (StartCount <= -3)
                {
                    sceneActive = true;
                    GameStart = false;
                    // -3秒になったときに１エリア下のコードに飛べるならなんでもいい。
                    // breakみたいな機能を持ってるやつない？
                } 
            }
        }
        



        // CountDownが1以下になるとreturnってどこに行くんだ？
        if (CountDown <= 1f)
        {
            return;
        }


        // sceneActiveってなんだろう、これfalseにしとけばタイマー止まるか？
        // StopTimerでfalseになってるし多分、なる
        // Playerたちが操作できないようにするのは後で考える？

        if (sceneActive==true)
        {
            CountDown -= Time.deltaTime;
            
            int second = (int)CountDown;
            TimerText.text = second.ToString();
            if (CountDown <= 1f)
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
