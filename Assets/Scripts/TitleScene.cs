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
        // 初回のデバイス接続状態の確認とパネルの表示・非表示
        //UpdateControllerStatus();

        // コントローラーの接続状態を確認するためのイベントハンドラを登録
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
    //        // デバイスの接続状態が変化したら、リストを更新するなどの処理を行う
    //        UpdateControllerStatus();
    //    }
    //}

    //private void UpdateControllerStatus()
    //{
    //    List<InputDevice> currentDevices = new List<InputDevice>(InputSystem.devices);

    //    // ここで特定のデバイス（例: キーボードやペン）を除外する処理を追加
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
