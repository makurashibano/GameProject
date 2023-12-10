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
            Vector3 hitPos = collision.contacts[0].point;
            if (collision.gameObject.transform.position.y + 0.3f > hitPos.y) return;
            collision.gameObject.GetComponent<Player>().isStan = true;
        }
        if (collision.gameObject.tag != "Stone")
        {
            this.gameObject.GetComponent<MeshCollider>().enabled = false;

            Invoke("ObjDestroy", stoneDestroy);
        }
    }
    void ObjDestroy()
    {
        
        Destroy(this.gameObject);
    }
}
