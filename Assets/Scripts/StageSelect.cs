using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public void SelectOcean()
    {
        SceneManager.LoadScene("Stage");
    }
    public void SelectVolcano()
    {
        SceneManager.LoadScene("FallFloorStage");
    }
}
