using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Touch_main : MonoBehaviour {

    public Text touch_state;

    public Text touch11;
    public Text touch22;
    public Text touch33;


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
    public float zoom_v;
    public Vector2 rotate_v;
    public float length_v;


    // Use this for initialization
    void Start () {
        touch1 = 0;
        touch2 = 0;

        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
        g_type = "none";
        zoom_v = 0;
        rotate_v = new Vector2(0f, 0f);
        length_v = 0;

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
                    touch1 = 1;
                    break;
                default:
                    touch1 = 0;
                    break;
            }

            if (count == 2)
            {
                switch (Input.GetTouch(1).phase)
                {
                    case TouchPhase.Began:
                        begin2 = Input.GetTouch(1).position;
                        end2 = begin2;
                        break;
                    case TouchPhase.Moved:
                        move2 = Input.GetTouch(1).position;
                        end2 = move2;
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
                //rotaing
                if (count == 1)
                {
                    Vector2 change = end1 - begin1;
                    if (change.magnitude > 100f)
                    {

                        rotate_v = change;
                        touch11.text = "panning";
                    }
                    else
                    {
                        rotate_v = new Vector2(0,0);

                    }
                }
                //zooming
                if (count == 2)
                {
                    Vector2 change1 = end1 - begin1;
                    Vector2 change2 = end2 - begin2;

                    touch11.text = "x:" + change1.x + ",  y:" + change1.y;
                    touch22.text = "x:" + change2.x + ",  y:" + change2.y;

                    if (change1.magnitude>5f && change2.magnitude > 5f)
                    {
                        float angle = Mathf.Acos(Vector3.Dot(change1, change2) / change1.magnitude / change2.magnitude);
                        //angle = Mathf.Min(angle, Mathf.PI - angle);

                        touch33.text = "angle:" + angle;

                        if(angle > Mathf.PI*2/3 && angle <= Mathf.PI){ //spread, pinch
                            if((begin1-begin2).magnitude > (end1 - end2).magnitude)// small size
                            {
                                zoom_v = 2.0f;
                            }
                            else
                            {
                                zoom_v = -2.0f;
                            }
                        }
                        else
                        {
                            zoom_v = 0f;
                        }
                    }
                    else
                    {
                        zoom_v = 0f;
                    }

                }
                else
                {
                    zoom_v = 0f;
                }


                break;
            case 1:
                break;
            case 2:
                //sketch

                break;
            case 3:
                //change length
                if (feature_info.s_mode == 1) //extrusion
                {
                    if (count == 2)
                    {
                        Vector2 change = (end1 - begin1);
                        //change.y *= 0.02f; //sensitivity
                        change.y = Mathf.Round(change.y*10) * 0.1f;
                        if (change.y >= 20f || change.y <= -20f)
                        {
                            length_v = change.y *0.02f;
                            g_type = "change";
                        }
                        else
                        {
                            length_v = 0;
                            g_type = "none";
                        }
                    }
                    else
                    {
                        length_v = 0;
                    }
                }
                break;
            default:
                break;
        }

       
		
	}
    
}
