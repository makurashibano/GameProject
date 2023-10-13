using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class HudManager : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropdown;
    public GameObject playerNumAddPanel;
    [SerializeField]
    PlayerInputManager PlayerInputManager;
    [SerializeField]
    private GameObject player;

    public void AddButton()
    {
        Invoke("JoinPlayer",0f);
        playerNumAddPanel.SetActive(false);
    }
    private void JoinPlayer()
    {
        if (dropdown.value == 0)
        {
            PlayerInputManager.instance.JoinPlayer(0, -1, null);
        }
        else
        {
            for (int i = 0; i < dropdown.value; i++)
            {
                PlayerInputManager.instance.JoinPlayer(i, -1, null);
                Instantiate(player, new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);
            }
        }
    }
}