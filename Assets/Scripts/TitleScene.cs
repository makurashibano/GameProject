using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    float timer = 0f;
    public GameObject CreditPanel;
    [SerializeField]
    PlayerInputManager PlayerInputManager;
    List<InputDevice> devices;
    public GameObject noControllerPanel;
    bool flag = true;
    private void Start()
    {
        // ����̃f�o�C�X�ڑ���Ԃ̊m�F�ƃp�l���̕\���E��\��
        //UpdateControllerStatus();

        // �R���g���[���[�̐ڑ���Ԃ��m�F���邽�߂̃C�x���g�n���h����o�^
        //InputSystem.onDeviceChange += OnDeviceChange;
    }
    private void Update()
    {
        if (SceneManager.GetSceneByName("Title").IsValid())
        {
            if (Gamepad.current.startButton.isPressed)
            {
                List<InputDevice> currentdevices = new List<InputDevice>(InputSystem.devices);

                if (currentdevices.Count > 1)
                {
                    CreditPanel.SetActive(false);
                    if (flag == true)
                    {
                        SceneManager.LoadScene("StageSelect");
                    }
                }
            }

        }
        timer += Time.deltaTime;

        if (timer >= 10.0f)
        {
            CreditPanel.SetActive(false);
        }
        if (timer >= 10f && timer <= 16f)
        {
            CreditPanel.SetActive(true);
        }
        if (timer >= 16.0f)
        {
            CreditPanel.SetActive(false);
            timer = 0f;
        }

    }
    //private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    //{
    //    if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
    //    {
    //        // �f�o�C�X�̐ڑ���Ԃ��ω�������A���X�g���X�V����Ȃǂ̏������s��
    //        UpdateControllerStatus();
    //    }
    //}

    //private void UpdateControllerStatus()
    //{
    //    List<InputDevice> currentDevices = new List<InputDevice>(InputSystem.devices);

    //    // �����œ���̃f�o�C�X�i��: �L�[�{�[�h��y���j�����O���鏈����ǉ�
    //    currentDevices.RemoveAll(device => device.name.Contains("Keyboard") || device.name.Contains("Mouse") || device.name.Contains("Pen"));

    //    if (currentDevices.Count > 1)
    //    {
    //        noControllerPanel.SetActive(false);
    //        flag = true;
    //    }
    //    else
    //    {
    //        noControllerPanel.SetActive(true);
    //        flag = false;
    //    }
   // }
}
