using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TouchTriggerPoint : MonoBehaviour
{
    private static int triggeredTimes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    { 
        var collisionPoint = collider.ClosestPoint(transform.position);
        
    }
}
