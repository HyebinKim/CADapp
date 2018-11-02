using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct plane_def
{
    public Vector3 point;
    public Vector3 normal;
}

public struct circle_def
{
    public Vector3 center;
    public Vector2 radius;
}

public class Main_code : MonoBehaviour {

    public int m_mode;
    // m_mode
    // 0: main display
    // 1: selection mode
    // 2: sketch mode
    // 3; solid mode
    public int s_mode;
    // 1-0: not selected
    // 1-1: plane selection
    // 1-2: sketch selection

    // 2-0: not operable
    // 2-1: rectangle
    // 2-2: circle
    // 2-3: polygon

    // 3-0: not operable
    // 3-1: extrusion
    // 3-2: cut extrusion
   

    // Button object
    public GameObject rectangle;
    public GameObject circle;
    public GameObject poly;
    public GameObject reset;

    public GameObject b_sketch;
    public GameObject b_main;
    public GameObject b_solid;
    public GameObject b_extrusion;

    //state
    public int s_feature;


    //data structure//
    //plane
    public plane_def xy;
    public plane_def yz;
    public plane_def zx;

    //circle
    public circle_def cir;

    //vertex
    public Vector3[] rec = new Vector3[4]; //vector3[2] or vector3[4] ??????
    


	// Use this for initialization
	void Start () {
        m_mode = 0;
        s_mode = 0;

        rectangle.SetActive(false);
        circle.SetActive(false);
        poly.SetActive(false);
        reset.SetActive(false);
        b_main.SetActive(false);

        //xy, yz, zx plane definition
        xy.point = new Vector3(0, 0, 0); xy.normal = new Vector3(0, 0, 1);
        yz.point = new Vector3(0, 0, 0); yz.normal = new Vector3(1, 0, 0);
        zx.point = new Vector3(0, 0, 0); zx.normal = new Vector3(0, 1, 0);

        s_feature = 0;

    }
	
	// Update is called once per frame
	void Update () {

        switch (m_mode)
        {
            case 0:
                b_sketch.SetActive(true);
                b_solid.SetActive(true);

                b_main.SetActive(false);
                rectangle.SetActive(false);
                circle.SetActive(false);
                poly.SetActive(false);
                reset.SetActive(false);
                b_extrusion.SetActive(false);
                break;

            case 1:
                break;

            case 2:
                //sketch button display
                rectangle.SetActive(true);
                circle.SetActive(true);
                poly.SetActive(true);
                reset.SetActive(true);
                b_main.SetActive(true);

                b_sketch.SetActive(false);
                b_solid.SetActive(false);

                break;

            case 3:
                b_main.SetActive(true);
                b_extrusion.SetActive(true);

                b_solid.SetActive(false);
                b_sketch.SetActive(false);

                break;

            default:
                break;
        }


	}

    public void Change_mMode(int i)
    {
        m_mode = i;
    }

    public void Change_sMode(int i)
    {
        s_mode = i;
    }

}
