using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDownManager : MonoBehaviour
{

    [SerializeField]
    float StartCount = 6f;
    // カウントダウンを開始する時間
    public TextMeshProUGUI StartDownText;
    // どこを参照するか
    public bool GameStart = true;
    // ゲームが始まってるかどうかの判定

    void Start()
    {
        GameStart = false;
        // 「start!!」が出る前は始まってないということにしたいので、コードの始まりはfalseにしておく
    }
    void Update()
    {
        int second = (int)StartCount;
        StartDownText.text = second.ToString();
        // カウントダウンを文字列に変更
        StartCount -= Time.deltaTime;

        if (StartCount < 1)
        {
            
            StartDownText.text = "start!!";
            // startの表示（3秒間）


            if (StartCount <= -2)
            {
                GameStart = true;
                StartDownText.text = " ";
                // start!!の表示が3秒経ったら、" "を表示する（テキストがなくなったわけではない）
            }
        }

    }
}
