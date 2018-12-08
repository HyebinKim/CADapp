﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class draw_sketch : MonoBehaviour {

    // mode 0: not select
    // mode 1: sketch_rectangle
    // mode 2: circle
    // mode 3: splines
    int click_count = 0;


    public Color c1 = Color.green;
    Vector3 start = new Vector3(0, 0, 0);
    Vector3 end = new Vector3(0, 0, 0);
    //Vector3[] positions;

    int segments = 60; //circle segments

    public Text ToWorld;

    public Text begin_;
    public Text end_;


    Main_code feature_info;
    Touch_main touch_info;

    Vector3 temp;

    LineRenderer rend;


    // Use this for initialization
    void Start () {
        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
        touch_info = GameObject.Find("Touch").GetComponent<Touch_main>();

        rend = GetComponent<LineRenderer>();
        rend.material = new Material(Shader.Find("Particles/Additive"));
        begin_.text = "one";

        rend.startColor = c1;
        rend.endColor = c1;
        rend.startWidth = 0.1f;
        rend.endWidth = 0.1f;

    }
	
	// Update is called once per frame
	void Update () {

        ToWorld.GetComponent<Text>();

        var v2 = Input.mousePosition;
        

        var v3 =Camera.main.ScreenToWorldPoint(new Vector3(v2.x, v2.y,15));

        ToWorld.text = "x=" + v3.x + "y=" + v3.z + "z=" + v3.y + "!!";

        int UI_click = 0;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)|| Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

        }

        //begin_.text = feature_info.m_mode.ToString();
        //end_.text = feature_info.s_mode.ToString();


        //if (feature_info.m_mode == 2 && UI_click==0)
        if (feature_info.m_mode == 2)
        {
            switch (feature_info.s_mode)
            {
                case 0: // not selected
                    end_.text = "nonselected";
                    break;

                case 1: //rectangle
                    begin_.text = "two";
                    LineRenderer rend = GetComponent<LineRenderer>();
                    Vector3[] positions = new Vector3[4];
                    rend.loop = true;
                    begin_.text = "three";

                    positions[0] = new Vector3(-2f, 0f, 2f);
                    positions[1] = new Vector3(-2f, 0f, -2f);
                    positions[2] = new Vector3(2f, 0f, -2f);
                    positions[3] = new Vector3(2f, 0f, 2f);
                    begin_.text = "four";

                    rend.positionCount = positions.Length;
                    rend.SetPositions(positions);
                    begin_.text = "five";


                    
                     // touch_info.single_update();

                    //if (touch_info.touch1 == 0) return;

                    
                    rend.material = new Material(Shader.Find("Particles/Additive"));

                    rend.startColor = c1;
                    rend.endColor = c1;
                    rend.startWidth = 0.1f;
                    rend.endWidth = 0.1f;

                    positions = new Vector3[4];
                    rend.loop = true;

                    Vector2 temp1 = new Vector2(500, 600);
                    Vector2 temp2 = new Vector2(600, 500);

                    //if (touch_info.count==1)
                    //{
                    //    temp2= touch_info.touch_point;
                   // }


                    feature_info.rec_plane = feature_info.nowP;

                    //start = Camera.main.ScreenToWorldPoint(new Vector3(touch_info.begin1.x, touch_info.begin1.y, 15));
                    //end = Camera.main.ScreenToWorldPoint(new Vector3(touch_info.end1.x, touch_info.end1.y, 15));

                    start = Camera.main.ScreenToWorldPoint(new Vector3(temp1.x, temp1.y, 15));
                    end = Camera.main.ScreenToWorldPoint(new Vector3(temp2.x, temp2.y, 15));

                    begin_.text = "pos:" + start.x + "  " + start.y + "  " + start.z;
                    end_.text = "pos:" + end.x + "  " + end.y + "  " + end.z;

                    positions[0] = start;
                    positions[1] = start + Vector3.Dot(feature_info.nowP.v, (end - start)) * feature_info.nowP.v;
                    positions[2] = end;
                    positions[3] = start + Vector3.Dot(feature_info.nowP.u, (end - start)) * feature_info.nowP.u;

                    rend.positionCount = positions.Length;
                    rend.SetPositions(positions);

                    for (int i = 0; i < 4; i++)
                    {
                        feature_info.rec[i] = positions[i];
                    }
                     
                     

                    
                     if (Input.GetMouseButtonDown(0))
                    {
                        click_count = 1;
                        start = v3;


                    }
                    if (Input.GetMouseButton(0))
                    {
                        if (click_count != 1)
                            break;
                        end = v3;

                        positions[0] = start;
                        positions[1] = start + Vector3.Dot(feature_info.nowP.v, (v3 - start)) * feature_info.nowP.v;
                        positions[2] = end;
                        positions[3] = start + Vector3.Dot(feature_info.nowP.u, (v3 - start)) * feature_info.nowP.u;

                        rend.positionCount = positions.Length;
                        rend.SetPositions(positions);
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        if (click_count != 1)
                            break;
                        end = v3;

                        positions[0] = start;
                        positions[1] = start + Vector3.Dot(feature_info.nowP.v, (v3 - start)) * feature_info.nowP.v;
                        positions[2] = end;
                        positions[3] = start + Vector3.Dot(feature_info.nowP.u, (v3 - start)) * feature_info.nowP.u;

                        rend.positionCount = positions.Length;
                        rend.SetPositions(positions);

                        for (int i = 0; i < 4; i++)
                        {
                            feature_info.rec[i] = positions[i];
                        }
                    }

                    
                     
                     



                    break;
                
                //circle
                case 2:

                    end_.text = "circle";

                    
                     LineRenderer rend2 = GetComponent<LineRenderer>();
                    rend2.material = new Material(Shader.Find("Particles/Additive"));

                    rend2.startColor = c1;
                    rend2.endColor = c1;
                    rend2.startWidth = 0.1f;
                    rend2.endWidth = 0.1f;

                    positions = new Vector3[segments];
                    rend2.loop = true;

                    Vector3 center = new Vector3(0f, 0f, 0f);
                    Vector2 radius = new Vector2(0f, 0f);

                    feature_info.cir.cir_plane = feature_info.nowP;

                    if (Input.GetMouseButtonDown(0))
                    {

                        click_count = 1;

                        start = v3;

                    }
                    if (Input.GetMouseButton(0))
                    {
                        if (click_count != 1)
                            break;
                        end = v3;

                        center = (start + end) / 2;

                        radius.x = Vector3.Dot(feature_info.nowP.u, (v3 - start)) / 2;
                        radius.y = Vector3.Dot(feature_info.nowP.v, (v3 - start)) / 2;

                        Vector3 temp;

                        float angle = 0.0f;

                        for (int i = 0; i < (segments); i++)
                        {
                            temp = center + Mathf.Cos(Mathf.Deg2Rad * angle) * radius.x * feature_info.nowP.u + Mathf.Sin(Mathf.Deg2Rad * angle) * radius.y * feature_info.nowP.v;
                            positions[i] = temp;

                            angle += (360f / segments);
                        }

                        rend2.positionCount = positions.Length;
                        rend2.SetPositions(positions);

                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        if (click_count != 1)
                            break;
                        end = v3;

                        center = (start + end) / 2;

                        radius.x = Vector3.Dot(feature_info.nowP.u, (v3 - start)) / 2;
                        radius.y = Vector3.Dot(feature_info.nowP.v, (v3 - start)) / 2;

                        Vector3 temp;

                        float angle = 0.0f;

                        for (int i = 0; i < (segments); i++)
                        {
                            temp = center + Mathf.Cos(Mathf.Deg2Rad * angle) * radius.x * feature_info.nowP.u + Mathf.Sin(Mathf.Deg2Rad * angle) * radius.y * feature_info.nowP.v;
                            positions[i] = temp;

                            angle += (360f / segments);
                        }

                        rend2.positionCount = positions.Length;
                        rend2.SetPositions(positions);

                        feature_info.cir.center = center;
                        feature_info.cir.radius = radius;
                    }
                     
                     
                     



                    break;

                //polygonal
                case 3:
                    //var pos3;


                    break;
                default:
                    break;
            }
        }

    }

    

}
