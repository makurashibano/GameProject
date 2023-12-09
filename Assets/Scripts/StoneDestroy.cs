using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Invoke("ObjDestroy", 0.2f);
    }
    void ObjDestroy()
    {
        Destroy(this.gameObject);
    }
}
