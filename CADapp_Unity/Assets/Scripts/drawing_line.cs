using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drawing_line : MonoBehaviour {

    public Text obj2;
    private Vector2 Mouse;
    private bool state;
    private int counts;

    public LineRenderer rend;

    Vector3 start = new Vector3(100, 100, 0);
    Vector3 end = new Vector3(-100, -100, 0);


    // Use this for initialization
    void Start () {
        state = false;
        obj2.text = "off";

        counts = 0;
        rend = GetComponent<LineRenderer>();

        rend.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButtonDown(0))
        {
            state = true;
            rend.enabled = true;
            

            Mouse = Input.mousePosition;
            start.x = Mouse.x;
            start.y = Mouse.y;
        
        }
        if (Input.GetMouseButton(0))
        {
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            state = false;
            rend.enabled = false;
        }


    }

    void SetLine(Vector3 start, Vector3 end)
    {
        rend.SetPosition(0, start);
        rend.SetPosition(1, end);
    }
}
