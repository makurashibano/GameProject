using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDestroy : MonoBehaviour
{
    public List<GameObject> stones = new List<GameObject>();

    public float stoneDestroy = 0.2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //�ڐG�|�C���g
            Vector3 hitPos = collision.contacts[0].point;
            //����艺�œ��������ꍇ�X�^�����Ȃ��悤�ɂ���
            if (collision.gameObject.transform.position.y + 0.1f > hitPos.y) return;
            //�ڐG�����v���C���[���X�^��������
            collision.gameObject.GetComponent<Player>().isStan = true;
        }
        if (collision.gameObject.tag != "Stone")
        {
            //�v���C���[�ƐڐG���Ȃ��悤�Ƀ��C���[��ύX
            this.gameObject.layer = 12;
            foreach (GameObject stone in stones)
            {
                stone.gameObject.layer = 12;
            }

            Invoke("ObjDestroy", stoneDestroy);
        }
    }
    void ObjDestroy()
    {        
        Destroy(this.gameObject);
    }
}
