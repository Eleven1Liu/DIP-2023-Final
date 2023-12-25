using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line_drawer : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer line;
    Vector3 star1_pos,star2_pos,star3_pos,star4_pos,star5_pos;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        SetupLine();
    }

    // Update is called once per frame
    void SetupLine(){
        line.positionCount = 6;
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        star1_pos = GameObject.Find("star1").transform.position;
        star2_pos = GameObject.Find("star2").transform.position;
        star3_pos = GameObject.Find("star3").transform.position;
        star4_pos = GameObject.Find("star4").transform.position;
        star5_pos = GameObject.Find("star5").transform.position;
        line.SetPosition(0,star2_pos);
        line.SetPosition(1,star1_pos);
        line.SetPosition(2,star3_pos);
        line.SetPosition(3,star1_pos);
        line.SetPosition(4,star4_pos);
        line.SetPosition(5,star5_pos);
    }
}
