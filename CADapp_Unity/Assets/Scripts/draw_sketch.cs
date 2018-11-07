using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class draw_sketch : MonoBehaviour {

    // mode 0: not select
    // mode 1: sketch_rectangle
    // mode 2: circle
    // mode 3: splines
    int click_count = 0;


    public Color c1 = Color.green;
    Vector3 start = new Vector3(-1, -1, 0);
    Vector3 end = new Vector3(1, 1, 0);
    Vector3[] positions;

    public int segments = 60; //circle segments

    public Text ToWorld;

    Main_code feature_info;

    // Use this for initialization
    void Start () {
        
        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
    }
	
	// Update is called once per frame
	void Update () {
        //but_rectangle.GetComponent<Button>();
        //but_rectangle.gameObject.SetActive(true);
        //but_rectangle.onClick.AddListener(ModeChange);
        ToWorld.GetComponent<Text>();

        var v3 = Input.mousePosition;
        v3.z = 15.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        v3.z = 0.0f;

        ToWorld.text = "x="+v3.x +"y="+ v3.y +"!!";

        //LineRenderer rend = GetComponent<LineRenderer>();

        if (feature_info.m_mode == 2)
        {
            switch (feature_info.s_mode)
            {
                case 0: // not selected
                    feature_info.s_feature = 0;
                    //rend.enabled = false;
                    break;

                case 1: //rectangle
                        //rend.enabled = true;
                    feature_info.s_feature = 1;

                    LineRenderer rend = GetComponent<LineRenderer>();
                    rend.material = new Material(Shader.Find("Particles/Additive"));

                    rend.startColor = c1;
                    rend.endColor = c1;
                    rend.startWidth = 0.1f;
                    rend.endWidth = 0.1f;

                    positions = new Vector3[4];
                    rend.loop = true;

                    if (Input.GetMouseButtonDown(0))
                    {
                        click_count = 1;

                        start = v3;
                        end = v3;

                        positions[0] = start;
                        positions[1] = new Vector3(start.x, end.y, 0);
                        positions[2] = end;
                        positions[3] = new Vector3(end.x, start.y, 0);

                        rend.positionCount = positions.Length;
                        rend.SetPositions(positions);

                    }
                    if (Input.GetMouseButton(0))
                    {
                        if (click_count != 1)
                            break;
                        end = v3;

                        positions[0] = start;
                        positions[1] = new Vector3(start.x, end.y, 0);
                        positions[2] = end;
                        positions[3] = new Vector3(end.x, start.y, 0);

                        rend.positionCount = positions.Length;
                        rend.SetPositions(positions);
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        if (click_count != 1)
                            break;
                        end = v3;

                        positions[0] = start;
                        positions[1] = new Vector3(start.x, end.y, 0);
                        positions[2] = end;
                        positions[3] = new Vector3(end.x, start.y, 0);

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
                    feature_info.s_feature = 2;

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

                    if (Input.GetMouseButtonDown(0))
                    {

                        click_count = 1;

                        start = v3;
                        end = v3;

                    }
                    if (Input.GetMouseButton(0))
                    {
                        if (click_count != 1)
                            break;
                        end = v3;

                        center.x = (start.x + end.x) / 2;
                        center.y = (start.y + end.y) / 2;

                        radius.x = Mathf.Abs(end.x - start.x) / 2;
                        radius.y = Mathf.Abs(end.y - start.y) / 2;

                        float x;
                        float y;
                        float z = 0f;

                        float angle = 0.0f;

                        for (int i = 0; i < (segments); i++)
                        {
                            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius.x + center.x;
                            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius.y + center.y;

                            positions[i] = new Vector3(x, y, z);

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

                        center.x = (start.x + end.x) / 2;
                        center.y = (start.y + end.y) / 2;

                        radius.x = Mathf.Abs(end.x - start.x) / 2;
                        radius.y = Mathf.Abs(end.y - start.y) / 2;

                        float x;
                        float y;
                        float z = 0f;

                        float angle = 0.0f;

                        for (int i = 0; i < (segments); i++)
                        {
                            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius.x + center.x;
                            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius.y + center.y;

                            positions[i] = new Vector3(x, y, z);

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
                    feature_info.s_feature = 3;
                    break;
                default:
                    break;
            }
        }

        

    }

}
