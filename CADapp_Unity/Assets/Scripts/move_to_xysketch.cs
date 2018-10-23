using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_to_sketch : MonoBehaviour {

    private Vector3 offset;
    Vector3 target_pos = new Vector3(0, 0, 15);

    void Start()
    {
        offset = transform.position - target_pos;
    }

    void LateUpdate()
    {
        transform.position = target_pos + offset;
    }
}
