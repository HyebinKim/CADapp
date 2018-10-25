using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draw_rec : MonoBehaviour {

    public Color c1 = Color.green;
    Vector3 start = new Vector3(-1, -1, 0);
    Vector3 end = new Vector3(1, 1, 0);
    Vector3[] positions = new Vector3[4];



    // Use this for initialization
    void Start () {

       // LineRenderer rend = gameObject.AddComponent<LineRenderer>();
       // rend.material = new Material(Shader.Find("Particles/Additive"));
        

        
        
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {

            LineRenderer rend = GetComponent<LineRenderer>();
           
            positions[0] = start;
            positions[1] = new Vector3(start.x, end.y, 0);
            positions[2] = end;
            positions[3] = new Vector3(end.x, start.y, 0);

            rend.positionCount = positions.Length;
            rend.SetPositions(positions);

            rend.material = new Material(Shader.Find("Particles/Additive"));
            rend.startColor = c1;
            rend.endColor = c1;
            rend.startWidth = 0.1f;
            rend.endWidth = 0.1f;

            rend.loop = true;
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

   
}
