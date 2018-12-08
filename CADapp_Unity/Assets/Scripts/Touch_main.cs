using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Touch_main : MonoBehaviour {

    public Text touch_state;
    public Text touch_pos;
    public Text touch_state2;


    public Vector2 touch_point;
    public int count;

    public int touch1; //0: no info, 1: get info
    public Vector2 begin1;
    public Vector2 move1;
    public Vector2 end1;

    public int touch2;
    public Vector2 begin2;
    public Vector2 move2;
    public Vector2 end2;



    // Use this for initialization
    void Start () {
        touch1 = 0;
        touch2 = 0;

    }
	
	// Update is called once per frame
	void Update () {

        count = Input.touchCount;

        switch (count)
        {
            case 0:
                touch_state.text = "none";
                break;

            case 1:
                touch_state.text = "single";
                touch_point = Input.GetTouch(0).position;
                touch_pos.text = "x=" + touch_point.x + " ,  y=" + touch_point.y;

                break;

            case 2:
                touch_state.text = "double";
                break;

            default:
                touch_state.text = "infinite";
                break;

        }
		
	}

    public void single_update()
    {
        if (Input.touchCount != 1)
            return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //value initialization
            begin1 = new Vector2();
            end1 = new Vector2();
            touch1 = 0;
            touch_state2.text = "begin";

            begin1 = Input.GetTouch(0).position;
            end1 = begin1;

        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            end1 = Input.GetTouch(0).position;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touch_state2.text = "end";
            end1 = Input.GetTouch(0).position;
            touch1 = 1;
        }


    }

}
