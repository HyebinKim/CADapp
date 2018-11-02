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
        }
        else
        {
            rend.enabled = false;
        }

    }
}
