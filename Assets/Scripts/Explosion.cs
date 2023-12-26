using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Awake() {
        // not shown in the beginning
        GetComponent<ParticleSystem>().playOnAwake = false;
    }
}
