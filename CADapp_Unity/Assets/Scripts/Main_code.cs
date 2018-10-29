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
    public float radius;
}

public class Main_code : MonoBehaviour {

    public int m_mode;
    // m_mode
    // 0: main display
    // 1: selection mode
    // 2: sketch mode
    public int s_mode;
    // 1-0: not selected
    // 1-1: plane selection
    // 1-2: sketch selection

    // 2-0: not operable
    // 2-1: rectangle
    // 2-2: circle

    public GameObject rectangle;
    public GameObject circle;

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

        //xy, yz, zx plane definition
        xy.point = new Vector3(0, 0, 0); xy.normal = new Vector3(0, 0, 1);
        yz.point = new Vector3(0, 0, 0); yz.normal = new Vector3(1, 0, 0);
        zx.point = new Vector3(0, 0, 0); zx.normal = new Vector3(0, 1, 0);


    }
	
	// Update is called once per frame
	void Update () {

        switch (m_mode)
        {
            case 0:
                break;

            case 1:
                break;

            case 2:
                //sketch button display
                rectangle.SetActive(true);
                circle.SetActive(true);



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
