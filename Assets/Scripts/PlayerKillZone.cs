using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKillZone : MonoBehaviour
{
    int playerCount;

    // Start is called before the first frame update
    void Start()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        Debug.Log(playerCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCount == 1)
        {
            SceneManager.LoadScene("Result", LoadSceneMode.Additive);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            --playerCount;
            Debug.Log(playerCount);
        }

    }

}
