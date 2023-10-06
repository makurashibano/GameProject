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

    public List<GameObject> PlayerSpawnPoint;
    public void AddButton()
    {
        Invoke("JoinPlayer",0f);
        playerNumAddPanel.SetActive(false);
    }
    private void JoinPlayer()
    {
        for (int i = 0; i <= dropdown.value; i++)
        {
            Debug.Log(i);
            PlayerInputManager.instance.JoinPlayer(i, -1, null);
            Instantiate(player, PlayerSpawnPoint[i].transform.position, Quaternion.identity);
        }
    }
}