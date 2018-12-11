using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class draw_sketch : MonoBehaviour {

    // mode 0: not select
    // mode 1: sketch_rectangle
    // mode 2: circle
    // mode 3: splines


    public Color c1 = Color.green;
    Vector3 start = new Vector3(0, 0, 0);
    Vector3 end = new Vector3(0, 0, 0);
    Vector3[] positions;

    int segments = 60; //circle segments

    public Text begin_;
    public Text end_;


    Main_code feature_info;
    Touch_main touch_info;

    line_def temp;


    // Use this for initialization
    void Start () {
        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
        touch_info = GameObject.Find("Touch").GetComponent<Touch_main>();

    }
	
	// Update is called once per frame
	void Update () {


        //if (feature_info.m_mode == 2 && UI_click==0)
        //drawing mode entering
        if (feature_info.m_mode == 2)
        {
            switch (feature_info.s_mode)
            {
                case 0: // not selected
                    
                    break;

                case 1: //rectangle

                    if (touch_info.count != 1) return;

                    LineRenderer rend = GetComponent<LineRenderer>();
                    rend.material = new Material(Shader.Find("Particles/Additive"));

                    rend.startColor = c1;
                    rend.endColor = c1;
                    rend.startWidth = 0.1f;
                    rend.endWidth = 0.1f;

                    positions = new Vector3[4];
                    rend.loop = true;

                    feature_info.rec_plane = feature_info.nowP;

                    start = Camera.main.ScreenToWorldPoint(new Vector3(touch_info.begin1.x, touch_info.begin1.y, 15));
                    end = Camera.main.ScreenToWorldPoint(new Vector3(touch_info.end1.x, touch_info.end1.y, 15));

                    begin_.text = "pos:" + start.x + "  " + start.y + "  " + start.z;
                    end_.text = "pos:" + end.x + "  " + end.y + "  " + end.z;

                    positions[0] = start;
                    positions[1] = start + Vector3.Dot(feature_info.nowP.v, (end - start)) * feature_info.nowP.v;
                    positions[2] = end;
                    positions[3] = start + Vector3.Dot(feature_info.nowP.u, (end - start)) * feature_info.nowP.u;

                    rend.positionCount = positions.Length;
                    rend.SetPositions(positions);

                    temp.type = 0;
                    temp.center = (start + end) / 2;

                    float mag1 = (positions[1] - positions[0]).magnitude;
                    float mag2 = (positions[2] - positions[1]).magnitude;
                    temp.radius.x = mag2 / 2;
                    temp.radius.y = mag1 / 2;

                    temp.plane = feature_info.nowP;

                    break;
                
                //circle
                case 2:
                    
                     if (touch_info.count != 1) return;

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

                    start = Camera.main.ScreenToWorldPoint(new Vector3(touch_info.begin1.x, touch_info.begin1.y, 15));
                    end = Camera.main.ScreenToWorldPoint(new Vector3(touch_info.end1.x, touch_info.end1.y, 15));

                    begin_.text = "pos:" + start.x + "  " + start.y + "  " + start.z;
                    end_.text = "pos:" + end.x + "  " + end.y + "  " + end.z;

                    center = (start + end) / 2;

                    radius.x = Vector3.Dot(feature_info.nowP.u, (end - start)) / 2;
                    radius.y = Vector3.Dot(feature_info.nowP.v, (end - start)) / 2;

                    Vector3 temp2;

                    float angle = 0.0f;

                    for (int i = 0; i < (segments); i++)
                    {
                        temp2 = center + Mathf.Cos(Mathf.Deg2Rad * angle) * radius.x * feature_info.nowP.u + Mathf.Sin(Mathf.Deg2Rad * angle) * radius.y * feature_info.nowP.v;
                        positions[i] = temp2;

                        angle += (360f / segments);
                    }

                    rend2.positionCount = positions.Length;
                    rend2.SetPositions(positions);


                    temp.type = 1;
                    temp.center = center;

                    float mag3 = (positions[1] - positions[0]).magnitude;
                    float mag4 = (positions[2] - positions[1]).magnitude;
                    temp.radius.x = radius.x;
                    temp.radius.y = radius.y;

                    temp.plane = feature_info.nowP;


                    break;

                //polygonal
                case 3:


                    break;
                default:
                    break;
            }
        }

    }

    public void sketch_export()
    {
        if (feature_info.m_mode == 2 && feature_info.s_mode !=0) //not complete
        {
            feature_info.line_collect.Add(temp);
        }
        
    }

    

}
