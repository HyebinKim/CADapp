using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drawing_line : MonoBehaviour {

    public Text obj2;
    private Vector2 Mouse;
    private bool state;

    Vector3 start = new Vector3(100, 100, 0);
    Vector3 end = new Vector3(-100, -100, 0);

    LineRenderer lr;

    // Use this for initialization
    void Start () {
        state = false;
        lr = GetComponent<LineRenderer>();
        SetLine(start, end);
        obj2.text = "off";
    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButtonDown(0))
        {
            state = true;
            Mouse = Input.mousePosition;
            start.x = Mouse.x;
            start.y = Mouse.y;
            //SetLine(start, start);
            obj2.text = "start:("+start.x+","+start.y+"), end:(" + end.x + "," + end.y + ")";
        }
        if (Input.GetMouseButton(0))
        {
            Mouse = Input.mousePosition;
            end.x = Mouse.x;
            end.y = Mouse.y;
            //SetLine(start, end);
            obj2.text = "start:(" + start.x + "," + start.y + "), end:(" + end.x + "," + end.y + ")";
        }
        if (Input.GetMouseButtonUp(0))
        {
            state = false;
            obj2.text = "start:(" + start.x + "," + start.y + "), end:(" + end.x + "," + end.y + ")";
            //SetLine(start, end);
        }


    }

    void SetLine(Vector3 start, Vector3 end)
    {
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.SetWidth(0.1f, 0.1f);
    }
}
