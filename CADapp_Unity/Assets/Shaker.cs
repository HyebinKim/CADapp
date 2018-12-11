using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shaker : MonoBehaviour
{
    Main_code feature_info;
    public Text shaking;

    void Start()
    {
        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
    }

    void Update()
    {
        if (feature_info.m_mode == 2)
        {


            switch (feature_info.s_mode)
            {
                case 0: // not selected
                    
                    break;

                case 1: //rectangle
                    if (Input.acceleration.x > 0f || Input.acceleration.y > 0f || Input.acceleration.z > 0f)
                    {
                        //feature_info.s_mode = 0;
                        shaking.text = "SHAKING!!!";
                    }
                    else
                    {
                        shaking.text = "not SHAKING!!!";
                    }
                    break;

                //circle
                case 2:
                    if (Input.acceleration.x > 0f || Input.acceleration.y > 0f || Input.acceleration.z > 0f)
                    {
                        //feature_info.s_mode = 0;
                        shaking.text = "SHAKING!!!";
                    }
                    else
                    {
                        shaking.text = "not SHAKING!!!";
                    }
                    break;

            }
        }
    }
}