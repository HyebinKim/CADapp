using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class draw_sketch : MonoBehaviour {

    public int mode = 0;
    // mode 0: not select
    // mode 1: sketch_rectangle
    // mode 2: circle
    // mode 3: splines

    public Button but_rectangle;

    public Color c1 = Color.green;
    Vector3 start = new Vector3(-1, -1, 0);
    Vector3 end = new Vector3(1, 1, 0);
    Vector3[] positions = new Vector3[4];

    public Text ToWorld;



    // Use this for initialization
    void Start () {
        mode = 1;
    }
	
	// Update is called once per frame
	void Update () {
        but_rectangle.GetComponent<Button>();
        but_rectangle.gameObject.SetActive(true);
        but_rectangle.onClick.AddListener(ModeChange);
        ToWorld.GetComponent<Text>();

        var v3 = Input.mousePosition;
        v3.z = 15.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        v3.z = 0.0f;

        ToWorld.text = "x="+v3.x +"y="+ v3.y +"!!";

        LineRenderer rend = GetComponent<LineRenderer>();

        rend.material = new Material(Shader.Find("Particles/Additive"));
        

        switch (mode)
        {
            case 0: // not selected
                break;
            case 1: //rectangle

                rend.startColor = c1;
                rend.endColor = c1;
                rend.startWidth = 0.1f;
                rend.endWidth = 0.1f;
                rend.loop = true;

                if (Input.GetMouseButtonDown(0))
                {
                    start = v3;
                    end = v3;

                    positions[0] = start;
                    positions[1] = new Vector3(start.x, end.y, 0);
                    positions[2] = end;
                    positions[3] = new Vector3(end.x, start.y, 0);

                    rend.positionCount = positions.Length;
                    rend.SetPositions(positions);

                }

                if (Input.GetMouseButton(0))
                {
                    end = v3;

                    positions[0] = start;
                    positions[1] = new Vector3(start.x, end.y, 0);
                    positions[2] = end;
                    positions[3] = new Vector3(end.x, start.y, 0);

                    rend.positionCount = positions.Length;
                    rend.SetPositions(positions);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    end = v3;

                    positions[0] = start;
                    positions[1] = new Vector3(start.x, end.y, 0);
                    positions[2] = end;
                    positions[3] = new Vector3(end.x, start.y, 0);

                    rend.positionCount = positions.Length;
                    rend.SetPositions(positions);
                }


                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }

        


        /*
         if (Input.GetMouseButtonDown(0))
        {
            start=Input.mousePosition;
            end = Input.mousePosition;
            RecGenerator(start, end);
        }
        if (Input.GetMouseButton(0))
        {
            end = Input.mousePosition;
            RecGenerator(start, end);
        }
        if (Input.GetMouseButtonUp(0))
        {
            end = Input.mousePosition;
            RecGenerator(start, end);
        }
         
         */


    }

    public void ModeChange()
    {
        //mode = 1;
        //Debug.Log("mode"+ mode);
        Debug.Log("Hello");
    }



}
