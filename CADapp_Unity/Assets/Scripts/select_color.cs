using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plane_select : MonoBehaviour {

    GameObject m_plane;
    Text m_Text;
    int count;

    void Start()
    {
        count = 0;
        m_plane = GetComponent<GameObject>();
        m_Text = GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            m_Text.color = Color.yellow;
        }
        else
        {
            m_Text.color = Color.white;
        }
    }
}
