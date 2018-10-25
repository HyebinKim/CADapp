using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_xy : MonoBehaviour {

    private float speed = 0.5f;
    Vector3 refPos;
    Vector3 targetPos;
    Quaternion targetRot;
    float moveSpeed = 0.05f;

    //GameObject xy;
    //GameObject yz;
    //GameObject zx;

    // Use this for initialization
    void Start () {
        refPos = new Vector3(15f, 15f, 15f);
        targetPos = new Vector3(0f, 0f, 15f);
        targetRot = new Quaternion(0, 90, 0, 1);
        //xy.GetComponent<GameObject>();
        //yz.GetComponent<GameObject>();
        //zx.GetComponent<GameObject>();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space"))
        {
            while (transform.position != targetPos)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, moveSpeed);
            }

            //yz.active = false;
            //zx.active = false;

            //Camera.main.transform.position = targetPos;
            //Camera.main.transform.rotation = targetRot;
        }


    }
}
