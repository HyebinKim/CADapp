using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroControl : MonoBehaviour
{
    Main_code feature_info;

    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    public int gyro_on;

    Vector3 refPos;
    Vector3 refRot;

    private void Start()
    {
        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();

        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        //gyroEnabled = EnableGyro();

        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);

            gyroEnabled = true;
        }
        else
        {
            gyroEnabled = false;
        }

        gyro_on = -1; //off

        refPos = new Vector3(15f, 15f, 15f);
        refRot = new Vector3(-1.0f, -1.0f, -1.0f);

    }

    public void EnableGyro()
    {
        gyro_on *= (-1);
    }

    void Update()
    {
        if (feature_info.m_mode == 0)
        {
            if (gyroEnabled && gyro_on==1)
            {
                transform.position = refPos;
                transform.rotation = Quaternion.LookRotation(refRot);

                transform.localRotation = gyro.attitude * rot;

                Camera.main.backgroundColor = Color.gray;
            }
            else
            {
                Camera.main.backgroundColor = Color.black;
            }
        }
        else
        {
            Camera.main.backgroundColor = Color.black;
        }
        
    }
}
