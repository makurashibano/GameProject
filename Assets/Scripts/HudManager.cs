using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class HudManager : MonoBehaviour
{
    [SerializeField]
    PlayerInputManager PlayerInputManager;
    private void Start()
    {
        List<InputDevice> devices = new List<InputDevice>(InputSystem.devices);
        devices.RemoveAll(devices => devices.name.Contains("Keyboard") || devices.name.Contains("Mouse") || devices.name.Contains("Pen"));
        for (int i = 0; i < devices.Count; i++)
        {
            PlayerInputManager.JoinPlayer(i, -1, null, devices[i]);
            Debug.Log(devices.Count +" _ "+ devices[i].name);
        }
    }
}