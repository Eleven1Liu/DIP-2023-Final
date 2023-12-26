using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star_material_changer : MonoBehaviour
{
    public Material targetMaterial;
    public float duration = 3f; // Time in seconds for the change to occur
    
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float t = (float)(Time.time - startTime) / (float)duration - (int)(Time.time - startTime) / (int) duration;
        targetMaterial.SetFloat("_Metallic", t);
    }
}
