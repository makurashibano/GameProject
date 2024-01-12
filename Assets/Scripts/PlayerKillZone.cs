using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //プレイヤーが落ちた場所にパーティクルを出現
            GameObject particle = Instantiate(other.gameObject.GetComponent<Player>().playerParticles[0], other.gameObject.transform.position, Quaternion.Euler(90, 0, 0));
            Destroy(particle, 2f);
        }
    }
}
