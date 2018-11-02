using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_display : MonoBehaviour {

    Main_code feature_info;

    // Use this for initialization
    void Start()
    {
        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
    }

    // Update is called once per frame
    void Update()
    {

        LineRenderer rend = GetComponent<LineRenderer>();

        //main screen
        if (feature_info.m_mode != 2)
        {
            switch (feature_info.s_feature)
            {
                case 0:
                    break;
                case 1:
                    //sketch rendering
                    rend.enabled = true;
                    rend.material = new Material(Shader.Find("Particles/Additive"));

                    rend.startColor = Color.red;
                    rend.endColor = Color.red;
                    rend.startWidth = 0.1f;
                    rend.endWidth = 0.1f;

                    rend.loop = true;

                    rend.positionCount = feature_info.rec.Length;
                    rend.SetPositions(feature_info.rec);
                    break;
                case 2:

                    rend.enabled = true;
                    rend.material = new Material(Shader.Find("Particles/Additive"));

                    rend.startColor = Color.red;
                    rend.endColor = Color.red;
                    rend.startWidth = 0.1f;
                    rend.endWidth = 0.1f;

                    int segments = 60;
                    Vector3[] position= new Vector3[segments];                   

                    float x;
                    float y;
                    float z = 0f;

                    float angle = 0.0f;

                    for (int i = 0; i < (segments); i++)
                    {
                        x = Mathf.Sin(Mathf.Deg2Rad * angle) * feature_info.cir.radius.x + feature_info.cir.center.x;
                        y = Mathf.Cos(Mathf.Deg2Rad * angle) * feature_info.cir.radius.y + feature_info.cir.center.y;

                        position[i] = new Vector3(x, y, z);

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
        else
        {
            rend.enabled = false;
        }

    }
}
