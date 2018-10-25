using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drawing_circle : MonoBehaviour {

    public Text obj2;
    private Vector2 Mouse;
    private bool state;
    public int segments;
    public float xradius;
    public float yradius;

    Vector3 start = new Vector3(100, 100, 0);
    Vector3 end = new Vector3(-100, -100, 0);

    LineRenderer lr;

    // Use this for initialization
    void Start()
    {
        state = false;
        lr = GetComponent<LineRenderer>();
        obj2.text = "off";
        lr.SetVertexCount(segments + 1);
        lr.useWorldSpace = false;
        CreatePoints();
    }

    
    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            state = true;
            Mouse = Input.mousePosition;
            float x = Mouse.x;
            float y = Mouse.y;
            CreatePoints();
            //SetLine(start, start);
            //obj2.text = "start:("+start.x+","+start.y+"), end:(" + end.x + "," + end.y + ")";
        }
        /*if (Input.GetMouseButton(0))
        {
            Mouse = Input.mousePosition;
            end.x = Mouse.x;
            end.y = Mouse.y;
            //SetLine(start, end);
            //obj2.text = "start:(" + start.x + "," + start.y + "), end:(" + end.x + "," + end.y + ")";
        }*/
        if (Input.GetMouseButtonUp(0))
        {
            state = false;
            //obj2.text = "start:(" + start.x + "," + start.y + "), end:(" + end.x + "," + end.y + ")";
            //SetLine(start, end);
        }


    }

    void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            lr.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}
