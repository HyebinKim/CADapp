using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_xy : MonoBehaviour {

    private float speed = 0.5f;
    Vector3 refPos;
    Vector3 targetPos;
    Quaternion targetRot;
    float moveSpeed = 0.05f;

    // Use this for initialization
    void Start () {
        refPos = new Vector3(15f, 15f, 15f);
        targetPos = new Vector3(0f, 0f, 15f);
        targetRot = new Quaternion(0, 90, 0, 1);

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            while (transform.position != targetPos)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, moveSpeed);
            }

            //Camera.main.transform.position = targetPos;
            //Camera.main.transform.rotation = targetRot;
        }


    }
}
