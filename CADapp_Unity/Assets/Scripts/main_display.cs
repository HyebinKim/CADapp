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
        //main screen
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

                    rend.positionCount = feature_info.rec.Length;
                    rend.SetPositions(feature_info.rec);
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

    }
}
