using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour {

   

    private Vector2 Mouse;
    private bool state;



    // Use this for initialization
    void Start()
    {
        state = false;
        GetComponent<Image>().color = new Color(0, 0, 0, 0);

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            state = true;
        }
        if (Input.GetMouseButton(0))
        {
            state = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            state = false;
        }


        if (state == true)
        {
            GetComponent<Image>().color = Color.white;
            Mouse = Input.mousePosition;
            this.transform.position = Mouse;
        }
        else
        {
            GetComponent<Image>().color = new Color(0,0,0,0); 
        }

        }
    }
