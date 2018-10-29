using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move_xy : MonoBehaviour {

    
    Vector3 refPos;
    Vector3 targetPos;
    Quaternion targetRot;
    Quaternion refRot;
    public float moveSpeed = 0.05f;

    //GameObject xy;
    //GameObject yz;
    //GameObject zx;

    // Use this for initialization
    void Start () {
        refPos = new Vector3(15f, 15f, 15f);
        refRot = Quaternion.Euler(35, -135, 0);

        targetPos = new Vector3(0f, 0f, 15f); //xy
        targetRot = Quaternion.Euler(0, 180, 0); //xy

        ToMain_Camera();

    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Move_Camera()
    {
        transform.position = targetPos;
        transform.rotation = targetRot;
        /*
         *  while (transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, moveSpeed);
        }
         */

    }

    public void ToMain_Camera()
    {
        transform.position = refPos;
        transform.rotation = refRot;
       
    }

}
