using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HudManager : MonoBehaviour
{
    [SerializeField]
    PlayerInputManager PlayerInputManager;
    private void Awake()
    {
        Invoke("JoinPlayer", 3f);
    }
    private void JoinPlayer()
    {
        PlayerInput playerInput = PlayerInputManager.JoinPlayer();
    }
}