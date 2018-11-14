using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move_camera : MonoBehaviour {

    
    Vector3 refPos;
    Vector3 refRot;
    public float moveSpeed = 0.05f;

    //GameObject xy;
    //GameObject yz;
    //GameObject zx;

    Main_code feature_info;

    // Use this for initialization
    void Start () {
        refPos = new Vector3(15f, 15f, 15f);
        refRot = new Vector3(-1.0f, -1.0f, -1.0f);

        ToMain_Camera();

        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();

    }
	
	// Update is called once per frame
	void Update () {
        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
    }

    public void ToMain_Camera()
    {
        transform.position = refPos;
        transform.rotation = Quaternion.LookRotation(refRot);

    }

    public void ToPlane_Camera()
    {
        Vector3 targetPos = feature_info.nowP.point + 15.0f * feature_info.nowP.normal;

        transform.position = targetPos;
        transform.rotation = Quaternion.LookRotation(-feature_info.nowP.normal, feature_info.nowP.v);

        Debug.Log("normal x=" + feature_info.nowP.normal.x + "y=" + feature_info.nowP.normal.y + "z=" + feature_info.nowP.normal.z);
        Debug.Log("up x=" + feature_info.nowP.v.x + "y=" + feature_info.nowP.v.y + "z=" + feature_info.nowP.v.z );
    }

}
