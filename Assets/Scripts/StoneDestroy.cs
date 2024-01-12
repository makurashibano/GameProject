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
            //接触ポイント
            Vector3 hitPos = collision.contacts[0].point;
            //頭より下で当たった場合スタンしないようにする
            if (collision.gameObject.transform.position.y + 0.1f > hitPos.y) return;
            //接触したプレイヤーをスタンさせる
            collision.gameObject.GetComponent<Player>().isStan = true;
        }
        if (collision.gameObject.tag != "Stone")
        {
            //プレイヤーと接触しないようにレイヤーを変更
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
