using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct plane_def
{
    public Vector3 point;
    public Vector3 normal;

    public Vector3 u;
    public Vector3 v;
}

public struct circle_def
{
    public Vector3 center;
    public Vector2 radius; //x=u_radius, y=v_radius

    public plane_def cir_plane;
}

public struct feature_def
{
    public int type; //0: sketch rec, 1: sketch circle, 2: extrusion, 3: cut extrusion
    //public int number;
    public int parent_feature_number;

    public List<int> comp_numb; //type(line/ face)
    // rec: cube: 6 faces, cylinder: 3 faces
}

public struct line_def
{
    //public int number;
    public int type; //0: rec, 1: circle

    public Vector3 center;
    public Vector2 radius; //x=u_radius, y=v_radius

    public plane_def plane;
}

public struct face_def
{
    public int parent_feature_number;
    //public int number;
    public int type; //0: flat rec, 1: flat circle, 2: curve

    public plane_def plane;
    //point=center

    public Vector3 center;
    public Vector2 radius; //x=u_radius, y=v_radius

    public Mesh mesh;
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

    public GameObject b_view;

    public GameObject b_gyro;

    //state
    public int s_feature;


    //data structure//
    //plane
    public plane_def xy;
    public plane_def yz;
    public plane_def zx;

    //current plane
    public plane_def nowP;

    //UI
    public Text xy_plane;
    public Text yz_plane;
    public Text zx_plane;

    public Button xy_;
    public Button yz_;
    public Button zx_;

    //circle
    public circle_def cir;

    //rectangle
    public Vector3[] rec = new Vector3[4];
    public plane_def rec_plane;

    /// <summary>
    /// data base
    /// 1. feture
    /// 2. line: sketch
    /// 3. face: solid
    /// </summary>

    public List<feature_def> feature_collect;
    public List<line_def> line_collect;
    public List<face_def> face_collect;

    public int selected_num;

    public Text t_feature;
    public Text t_line;
    public Text t_face;

    // Use this for initialization
    void Start () {
        //screen
        Screen.SetResolution(1280, 720, true); //galaxy


        m_mode = 0;
        s_mode = 0;

        rectangle.SetActive(false);
        circle.SetActive(false);
        poly.SetActive(false);
        reset.SetActive(false);
        b_main.SetActive(false);
        b_extrusion.SetActive(false);


        //xy, yz, zx plane definition
        xy.point = new Vector3(0, 0, 0); xy.normal = new Vector3(0, 1, 0); xy.u = new Vector3(1, 0, 0); xy.v = new Vector3(0, 0, 1);
        yz.point = new Vector3(0, 0, 0); yz.normal = new Vector3(1, 0, 0); yz.u = new Vector3(0, 0, 1); yz.v = new Vector3(0, 1, 0);
        zx.point = new Vector3(0, 0, 0); zx.normal = new Vector3(0, 0, 1); zx.u = new Vector3(0, 1, 0); zx.v = new Vector3(1, 0, 0);

        s_feature = 0;

        nowP = xy;

        xy_.onClick.AddListener(delegate { Change_plane(xy.point, xy.normal, xy.u, xy.v); });
        yz_.onClick.AddListener(delegate { Change_plane(yz.point, yz.normal, yz.u, yz.v); });
        zx_.onClick.AddListener(delegate { Change_plane(zx.point, zx.normal, zx.u, zx.v); });

        feature_collect = new List<feature_def>();
        line_collect = new List<line_def>();
        face_collect=new List<face_def>();

        selected_num = -1; //non selected
    }
	
	// Update is called once per frame
	void Update () {

        
        t_feature.text = "feature:" + feature_collect.Count;
        t_line.text = "line:" + line_collect.Count;
        t_face.text = "face:" + face_collect.Count;


        //button activation
        switch (m_mode)
        {
            case 0:
                b_sketch.SetActive(true);
                b_solid.SetActive(true);
                b_view.SetActive(true);
                b_gyro.SetActive(true);

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
                b_view.SetActive(false);
                b_gyro.SetActive(false);

                break;

            case 3:
                b_main.SetActive(true);
                b_extrusion.SetActive(true);

                b_solid.SetActive(false);
                b_sketch.SetActive(false);
                b_view.SetActive(false);
                b_gyro.SetActive(false);

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

    //camera: now plane?
    public void Change_plane(Vector3 point, Vector3 normal, Vector3 u, Vector3 v)
    {
        nowP.point = point;
        nowP.normal = normal;
        nowP.u = u;
        nowP.v = v;
    }

    //feature tree
    public void Create_sketch()
    {
        if (m_mode != 2) return;
        Debug.Log("it'work");
        //sketch 객체 판별
        switch (s_mode)
        {
            case 0:
                s_feature = 0;
                break;
            case 1:
                s_feature = 1;
                break;
            case 2:
                s_feature = 2;
                break;
            case 3:
                s_feature = 3;
                break;
            default:
                break;

        }

        

    }

    //app exit
    public void Exit()
    {
        Application.Quit();
    }

}
