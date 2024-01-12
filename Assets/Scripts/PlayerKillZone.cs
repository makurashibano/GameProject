using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //�v���C���[���������ꏊ�Ƀp�[�e�B�N�����o��
            GameObject particle = Instantiate(other.gameObject.GetComponent<Player>().playerParticles[0], other.gameObject.transform.position, Quaternion.Euler(90, 0, 0));
            Destroy(particle, 2f);
        }
    }
}
