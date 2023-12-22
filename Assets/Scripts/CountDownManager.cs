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
    // �J�E���g�_�E�����J�n���鎞��
    public TextMeshProUGUI StartDownText;
    // �ǂ����Q�Ƃ��邩
    public bool GameStart = true;
    // �Q�[�����n�܂��Ă邩�ǂ����̔���

    void Start()
    {
        GameStart = false;
        // �ustart!!�v���o��O�͎n�܂��ĂȂ��Ƃ������Ƃɂ������̂ŁA�R�[�h�̎n�܂��false�ɂ��Ă���
    }
    void Update()
    {
        int second = (int)StartCount;
        StartDownText.text = second.ToString();
        // �J�E���g�_�E��
        StartCount -= Time.deltaTime;
        if (StartCount < 1)
        {
            
            StartDownText.text = "start!!";
            // �����������R�b�����\�����ĊJ�n����񂶂�ˁH
            

            if (StartCount <= -2)
            {
                GameStart = true;
                StartDownText.text = " ";

            }
        }

    }
}
