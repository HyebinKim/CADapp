using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_display : MonoBehaviour {

    Main_code feature_info;
    int segments = 60;

    public GameObject sketchs;
    List<LineRenderer> rend_collect;

    int active_mode;
    int current_number;

    // Use this for initialization
    void Start()
    {
        //line renderer setting

        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
        active_mode = 0; // not changing
        current_number = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //main screen

        //line drawing

        rend_collect = new List<LineRenderer>();

        if (feature_info.line_collect.Count > 0)
        {
            for(int i=0;i< feature_info.line_collect.Count; i++)
            {
                //LineRenderer rend = sketchs.AddComponent<LineRenderer>();
                LineRenderer rend = GetComponent<LineRenderer>();
                rend.enabled = true;
                rend.material = new Material(Shader.Find("Particles/Additive"));

                rend.startColor = Color.white;
                rend.endColor = Color.white;
                rend.startWidth = 0.1f;
                rend.endWidth = 0.1f;

                rend.loop = true;

                Vector3[] positions = calculate_position(feature_info.line_collect[i]);

                rend.positionCount = positions.Length;
                rend.SetPositions(positions);

                rend_collect.Add(rend);
            }
            //LineRenderer rend = GetComponent<LineRenderer>();
        }

        if(current_number != feature_info.line_collect.Count && current_number >0)
        {
            for (int i = 0; i < feature_info.line_collect.Count; i++)
            {
                GameObject temp2 = Instantiate(sketchs);
                LineRenderer rend2 = temp2.GetComponent<LineRenderer>();
                rend2.enabled = true;
                rend2.material = new Material(Shader.Find("Particles/Additive"));

                rend2.startColor = Color.white;
                rend2.endColor = Color.white;
                rend2.startWidth = 0.1f;
                rend2.endWidth = 0.1f;

                rend2.loop = true;

                Vector3[] positions = calculate_position(feature_info.line_collect[0]);

                rend2.positionCount = positions.Length;
                rend2.SetPositions(positions);
            }
               

            //rend2.SetPositions(rend_collect[feature_info.line_collect.Count - 1].G)
            //rend2 =rend_collect[feature_info.line_collect.Count-1];
            current_number++;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
           // GameObject temp3 = Instantiate(sketchs);
           // LineRenderer rend3 = temp3.GetComponent<LineRenderer>();
           // rend3.loop = false;
            //rend3.startWidth = rend3.startWidth * 10;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //GameObject temp2 = (GameObject)Instantiate(sketchs);
            //temp2.transform.Translate(new Vector3(0, 1, 0));
        }

        /*
         
         if (rend_collect.Count >0)
        {
            LineRenderer rend3 = GetComponent<LineRenderer>();
            rend3 = rend_collect[0];

            if (rend_collect.Count == 2)
            {
                GameObject temp = Instantiate(sketchs);
                LineRenderer rend2 = temp.GetComponent<LineRenderer>();
                rend2.loop = false;
                rend2.startColor = Color.red;
                rend2.endColor = Color.red;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject temp2 = (GameObject)Instantiate(sketchs);
            temp2.transform.Translate(new Vector3(0, 1, 0));
        }
         
         */



        //LineRenderer rend4 = GetComponent<LineRenderer>();

        //LineRenderer rend5 = temp2.GetComponent<LineRenderer>();

        //Destroy(temp2, Time.maximumDeltaTime);

        /*
         if (rend_collect.Count > 0)
        {
            LineRenderer rend = sketchs.AddComponent<LineRenderer>();
            for (int i=0;i< rend_collect.Count; i++)
            {
                LineRenderer rend2 = Instantiate(rend);
                rend2 = rend_collect[i];
            }

        }
         */



        /*
          if (feature_info.m_mode != 2 && feature_info.s_feature!=0)
        {
            LineRenderer rend = GetComponent<LineRenderer>();

            switch (feature_info.s_feature)
            {
                case 0:
                    break;
                case 1:
                    //sketch rendering
                    rend.enabled = true;
                    rend.material = new Material(Shader.Find("Particles/Additive"));

                    rend.startColor = Color.white;
                    rend.endColor = Color.white;
                    rend.startWidth = 0.1f;
                    rend.endWidth = 0.1f;

                    rend.loop = true;

                    Vector3[] positions = calculate_position(feature_info.line_collect[0]);

                    rend.positionCount = positions.Length;
                    rend.SetPositions(positions);
                    break;
                case 2:

                    rend.enabled = true;
                    rend.material = new Material(Shader.Find("Particles/Additive"));

                    rend.startColor = Color.white;
                    rend.endColor = Color.white;
                    rend.startWidth = 0.1f;
                    rend.endWidth = 0.1f;

                    rend.loop = true;

                    int segments = 60;
                    Vector3[] position= new Vector3[segments];

                    Vector3 temp;

                    float angle = 0.0f;

                    for (int i = 0; i < (segments); i++)
                    {
                        temp = feature_info.cir.center + Mathf.Cos(Mathf.Deg2Rad * angle) * feature_info.cir.radius.x * feature_info.cir.cir_plane.u + Mathf.Sin(Mathf.Deg2Rad * angle) * feature_info.cir.radius.y * feature_info.cir.cir_plane.v;
                        position[i] = temp;

                        angle += (360f / segments);
                    }

                    rend.positionCount = position.Length;
                    rend.SetPositions(position);

                    break;
                case 3:
                    break;
                default:
                    break;

            }
            
        }
         
         */


    }

    Vector3[] calculate_position(line_def sketch)
    {
        Vector3[] pos;
        if (sketch.type == 0) //rectangle
        {
            pos = new Vector3[4];

            pos[0] = sketch.center + sketch.plane.u * sketch.radius.x + sketch.plane.v * sketch.radius.y;
            pos[1] = sketch.center - sketch.plane.u * sketch.radius.x + sketch.plane.v * sketch.radius.y;
            pos[2] = sketch.center - sketch.plane.u * sketch.radius.x - sketch.plane.v * sketch.radius.y;
            pos[3] = sketch.center + sketch.plane.u * sketch.radius.x - sketch.plane.v * sketch.radius.y;

        }
        else //circle: sketch.type == 1
        {
            pos = new Vector3[segments];

        }

        return pos;
    }

}
