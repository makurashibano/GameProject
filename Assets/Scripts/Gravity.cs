using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float gravityPower = -10;
    Rigidbody rigidbody;    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 gravity = new Vector3(0f, gravityPower, 0f);
        rigidbody.AddForce(gravity);
    }
}
