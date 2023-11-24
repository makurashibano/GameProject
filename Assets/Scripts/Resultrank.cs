using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Resultrank : MonoBehaviour
{
    [SerializeField]
    List<GameObject> playerprefab;
    [SerializeField]
    List<Transform> playerrank;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Player.rank.Count; i++)
        {
            int pl = Player.rank[i];
            Instantiate(playerprefab[pl], playerrank[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.buttonEast.isPressed)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
