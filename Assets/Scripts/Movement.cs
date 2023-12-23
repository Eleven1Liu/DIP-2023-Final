using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 initialAngularVelocity;
    public Vector3 initialVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
        rb.angularVelocity = initialAngularVelocity;
    }
}
