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
        // �J�E���g�_�E���𕶎���ɕύX
        StartCount -= Time.deltaTime;

        if (StartCount < 1)
        {
            
            StartDownText.text = "start!!";
            // start�̕\���i3�b�ԁj


            if (StartCount <= -2)
            {
                GameStart = true;
                StartDownText.text = " ";
                // start!!�̕\����3�b�o������A" "��\������i�e�L�X�g���Ȃ��Ȃ����킯�ł͂Ȃ��j
            }
        }

    }
}
