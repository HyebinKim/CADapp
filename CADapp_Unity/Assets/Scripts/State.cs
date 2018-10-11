using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour {

    public Text obj;
    private Vector3 Mouse;
    private bool state;


    // Use this for initialization
    void Start () {
        obj.text = "off";
        state = false;
	}


    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            state = true;
        }
        if (Input.GetMouseButton(0))
        {
            state = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            state = false;
        }


        if (state == true)
        {
            Mouse = Input.mousePosition;
            Mouse.z = 0;
            obj.text = "x=" + Mouse.x + ", y=" + Mouse.y +", z=" + Mouse.z + "\n";
        }
        else
        {
            obj.text = "off";
        }

    }
}
