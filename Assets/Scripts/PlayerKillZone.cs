using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKillZone : MonoBehaviour
{
    bool flag = false;

    Collider PlayerKillZoneCol;

    private void Awake()
    {
        PlayerKillZoneCol = GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players= GameObject.FindGameObjectsWithTag("Player");
        int fallingplayercount = 0;

        foreach (var player in players)
        {
            Collider playercol = player.GetComponent<Collider>();
            if (PlayerKillZoneCol.bounds.Intersects(playercol.bounds))
            {
                ++fallingplayercount;
            }
        }
        //Debug.Log(players.Length+":"+fallingplayercount);

        if (fallingplayercount == 0&&(flag==false))
        {
            flag = true;
        }
        if (flag)
        {
            if (players.Length >= 2 && (players.Length - fallingplayercount == 1))
            {
                if (!SceneManager.GetSceneByName("Result").IsValid())
                {
                    SceneManager.LoadScene("Result", LoadSceneMode.Additive);
                    //リザルトシーンを消してタイトルシーンを追加
                    Time.timeScale = 0;
                    //Invoke("unloadresult", 3f);
                }
                flag = false;
            }
        }
    }
    void Unloadresult()
    {
        SceneManager.UnloadScene("Result");
    }
}
