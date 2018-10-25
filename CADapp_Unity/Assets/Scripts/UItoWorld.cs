using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItoWorld : MonoBehaviour {

    public Text mText;
    Vector3 wposition;
    Vector2 mPosition;

	// Use this for initialization
	void Start () {
        mText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        mPosition = Input.mousePosition;
        wposition = new Vector3(mPosition.x, mPosition.y, 0);
        mText.text = "x=" + wposition.x + ", y=" + wposition.y + ", z= " + wposition.z;
    }
}
