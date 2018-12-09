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

    Main_code feature_info;
    public string g_type;


    // Use this for initialization
    void Start () {
        touch1 = 0;
        touch2 = 0;

        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
        g_type = "none";
    }
	
	// Update is called once per frame
	void Update () {

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            touch_state.text = "UI!!!!";
            return;
        }


        count = Input.touchCount;

        //get touch information
        if(count==1 || count == 2)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    begin1 = Input.GetTouch(0).position;
                    end1 = begin1;
                    break;
                case TouchPhase.Moved:
                    move1 = Input.GetTouch(0).position;
                    end1 = move1;
                    break;
                case TouchPhase.Ended:
                    end1 = Input.GetTouch(0).position;
                    break;
                default:
                    break;
            }

            if (count == 2)
            {
                switch (Input.GetTouch(1).phase)
                {
                    case TouchPhase.Began:
                        begin2 = Input.GetTouch(1).position;
                        break;
                    case TouchPhase.Moved:
                        move2 = Input.GetTouch(1).position;
                        break;
                    case TouchPhase.Ended:
                        end2 = Input.GetTouch(1).position;
                        break;
                    default:
                        break;
                }
            }
        }


        //gesture identification
        switch (feature_info.m_mode)
        {
            case 0:
                if (count == 1)//rotaing
                {

                }
                
                if (count == 2)//zooming
                {

                }
                

                break;
            case 1:
                break;
            case 2:
                //sketch
                if (count == 1)
                {

                }

                break;
            case 3:
                //change length
                if (feature_info.s_mode == 1) //extrusion
                {

                }
                break;
            default:
                break;
        }

       
		
	}
    
}
