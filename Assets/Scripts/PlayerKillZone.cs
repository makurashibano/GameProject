using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillZone : MonoBehaviour
{
    public int particlenumber = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //�v���C���[���������ꏊ�Ƀp�[�e�B�N�����o��
            GameObject particle = Instantiate(other.gameObject.GetComponent<Player>().playerParticles[particlenumber], other.gameObject.transform.position, Quaternion.Euler(90, 0, 0));
            Destroy(particle, 2f);
        }
    }
}
