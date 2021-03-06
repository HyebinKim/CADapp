﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move_camera : MonoBehaviour {

    
    Vector3 refPos;
    Vector3 refRot;

    Vector3 nowPos;
    Vector3 nowRot;

    public float moveSpeed = 0.05f;
    public float OrbitDampening = 10f;

    //GameObject xy;
    //GameObject yz;
    //GameObject zx;
    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    Main_code feature_info;
    Touch_main touch_info;

    public float camera_viewsize = 5f;

    public GameObject target;
    public Vector3 point;

    // Use this for initialization
    void Start () {
        refPos = new Vector3(15f, 15f, 15f);
        refRot = new Vector3(-1.0f, -1.0f, -1.0f);

        nowPos = refPos;
        nowRot = refRot;

        ToMain_Camera();

        //this._XForm_Camera = this.transform;
        //_XForm_Parent.position = new Vector3(0, 0, 0);
        //_XForm_Parent.rotation= Quaternion.Euler(0, 0, 0);

        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
        touch_info = GameObject.Find("Touch").GetComponent<Touch_main>();

        Camera.main.orthographicSize = 5.0f;

        point = target.transform.position;
        transform.LookAt(point);
    }
	
	// Update is called once per frame
	void Update () {
        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();

        if (feature_info.m_mode == 0)
        {
            zooming(touch_info.zoom_v * 0.01f);

            if (touch_info.count == 1)
            {
                rotating(touch_info.rotate_v.x, touch_info.rotate_v.y);

            }
        }

        //rotating(touch_info.rotate_v.x, touch_info.rotate_v.y);

    }

    public void ToMain_Camera()
    {
        transform.position = refPos;
        transform.rotation = Quaternion.LookRotation(refRot);

        nowPos = refPos;
        nowRot = refRot;

    }

    public void ToPlane_Camera()
    {
        Vector3 targetPos = feature_info.nowP.point + 15.0f * feature_info.nowP.normal;

        transform.position = targetPos;
        transform.rotation = Quaternion.LookRotation(-feature_info.nowP.normal, feature_info.nowP.v);

        Debug.Log("normal x=" + feature_info.nowP.normal.x + "y=" + feature_info.nowP.normal.y + "z=" + feature_info.nowP.normal.z);
        Debug.Log("up x=" + feature_info.nowP.v.x + "y=" + feature_info.nowP.v.y + "z=" + feature_info.nowP.v.z );

    }

    //zoom in, out
    public void zooming(float d)
    {
        if (camera_viewsize + d* camera_viewsize <= 0.1)
            return;
        camera_viewsize += d* camera_viewsize;
        Camera.main.orthographicSize = camera_viewsize;

    }

    //rotating
    public void rotating(float _x, float _y)
    {
        //Vector3 change = new Vector3(Input.GetAxis("Vertical") * _y, Input.GetAxis("Horizontal") * _x, 0);
        Vector3 temp1 = Camera.main.transform.up * _x;
        Vector3 temp2 = Camera.main.transform.right * _y*(-1f);
        Vector3 change = temp1 + temp2;
        //Vector3 change = Camera.main.ScreenToWorldPoint(new Vector3(_x, _y, 15));
        transform.RotateAround(point, change, 1f);

        //transform.RotateAround(point, new Vector3(0.0f, 0.0f, 1.0f), 5 * 5f);

        //float _localRotX = Input.GetAxis("Mouse X")*_x;
        //float _localRotY = Input.GetAxis("Vertical")*_y;

        //Quaternion QT = Quaternion.Euler(_localRotY, _localRotX, 0);

        //this._XForm_Parent.rotation = Quaternion.Lerp(_XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);
        // this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));

    }

}
