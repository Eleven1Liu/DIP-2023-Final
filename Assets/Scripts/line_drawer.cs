using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line_drawer : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer line;
    public int PointCount;
    public float width;
    [SerializeField]
    private List<GameObject> pos = new List<GameObject>();
    void Start()
    {
        line = GetComponent<LineRenderer>();
        SetupLine();
    }

    // Update is called once per frame
    void SetupLine(){
        line.positionCount = PointCount;
        line.startWidth = width;
        line.endWidth = width;
        for(int i = 0 ; i < PointCount - 1 ; i++){
            connect(i,pos[i],pos[i+1]);
        }
        
    }
    void connect(int i ,GameObject ob1, GameObject ob2){
        line.SetPosition(i,ob1.transform.position);
        line.SetPosition((i+1),ob2.transform.position);
    }
}
