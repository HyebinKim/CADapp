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

            if(feature_info.s_mode != 0)
            {
                if (Input.acceleration.sqrMagnitude > 5f)
                {
                    feature_info.s_mode = 0;
                    shaking.text = "SHAKING!!!";
                }
                else
                {
                    shaking.text = "not SHAKING!!!";
                }
            }
            else
            {
                shaking.text = "not SHAKING!!!";
            }


            
        }
    }
}