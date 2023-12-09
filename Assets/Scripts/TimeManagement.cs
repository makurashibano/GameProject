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
            // �������Ԃ̂���J�E���g�_�E��������A5�b�Ԃ������𐔂��ĉ��̃R�[�h�ɓn���݂����ȏ�������肽��
            // ���Ԃ񂻂�����΂������̏������ɂ��邩��A���ʓI�ɒN�������Ȃ����ԂɂȂ�񂶂�Ȃ��̂��H
            // �v���C���[�L���������������̂͂ǂ̃^�C�~���O�H
            
            int second = (int)StartCount;


            // ���Ƃ͂����ɃJ�E���g�_�E�����T�b����n�߂�������Ί���


            TimerText.text = second.ToString();

            if(StartCount <= 0)
            {
                TimerText.text = "start!";
                // �����������R�b�����\�����ĊJ�n����񂶂�ˁH

                if (StartCount <= -3)
                {
                    sceneActive = true;
                    GameStart = false;
                    // -3�b�ɂȂ����Ƃ��ɂP�G���A���̃R�[�h�ɔ�ׂ�Ȃ�Ȃ�ł������B
                    // break�݂����ȋ@�\�������Ă��Ȃ��H
                } 
            }
        }
        



        // CountDown��1�ȉ��ɂȂ��return���Ăǂ��ɍs���񂾁H
        if (CountDown <= 1f)
        {
            return;
        }


        // sceneActive���ĂȂ񂾂낤�A����false�ɂ��Ƃ��΃^�C�}�[�~�܂邩�H
        // StopTimer��false�ɂȂ��Ă邵�����A�Ȃ�
        // Player����������ł��Ȃ��悤�ɂ���̂͌�ōl����H

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
