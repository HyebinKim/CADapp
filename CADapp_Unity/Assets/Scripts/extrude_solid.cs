using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class extrude_solid : MonoBehaviour
{
    MeshFilter filter;
    new MeshRenderer renderer;

    public MeshFilter Filter
    {
        get
        {
            if (filter == null)
            {
                filter = GetComponent<MeshFilter>();
            }
            return filter;
        }
    }
    public MeshRenderer Renderer
    {
        get
        {
            if (renderer == null)
            {
                renderer = GetComponent<MeshRenderer>();
            }
            return renderer;
        }
    }
    
    //Range(0.1f, 10f)
    public float height = 3f;
    public float radius = 1f;
    //Range(3, 32)
    public int segments = 16;
    //
    public bool openEnded = true;
    public Vector3 center;
    const float PI2 = Mathf.PI * 2f;

    public Slider heightSlider;
    //public Slider radiusSlider;
    //public Slider segmentsSlider;

    // mode 0: not select
    // mode 1: sketch_rectangle
    // mode 2: circle
    // mode 3: splines
    //int click_count = 0;
    Main_code feature_info;
    
    // Use this for initialization
    void Start()
    {
        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //LineRenderer rend = GetComponent<LineRenderer>();
        if (feature_info.m_mode == 3)
        {

            switch (feature_info.s_mode)
            {
                case 0: //not selected
                    break;
                case 1: //extrusion

                    switch (feature_info.s_feature)
                    {
                        case 0: // not selected
                            feature_info.s_feature = 0;
                            //rend.enabled = false;
                            break;
                        case 1: //rectangle
                              
                            //Build a mesh
                            Filter.sharedMesh = Build2();


                            break;
                             
                            


                        case 2://circle
                            feature_info.s_feature = 2;

                            segments = 16;
                            radius = feature_info.cir.radius.x;
                            center = feature_info.cir.center;

                            Filter.sharedMesh = Build();

                            break;
                        default:
                            break;
                    }


                    break;

                case 2: //cut extrusion
                    break;
                default:
                    break;
            }


           
        }
    }

    Mesh Build2()
    {
        var mesh2 = new Mesh();

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();
        var triangles = new List<int>();

        float length=5.0f;

        vertices.Add(feature_info.rec[0]);
        vertices.Add(feature_info.rec[1]);
        vertices.Add(feature_info.rec[2]);
        vertices.Add(feature_info.rec[3]);

        uvs.Add(new Vector2(0f, 0f));
        uvs.Add(new Vector2(1f, 0f));
        uvs.Add(new Vector2(1f, 1f));
        uvs.Add(new Vector2(0f, 1f));

        normals.Add(new Vector3(0f, 1f, 1f));
        normals.Add(new Vector3(0f, 1f, 1f));
        normals.Add(new Vector3(0f, 1f, 1f));
        normals.Add(new Vector3(0f, 1f, 1f));


        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);
        triangles.Add(2);
        triangles.Add(3);
        triangles.Add(0);

        mesh2.vertices = vertices.ToArray();
        mesh2.uv = uvs.ToArray();
        mesh2.normals = normals.ToArray();
        mesh2.triangles = triangles.ToArray();
        mesh2.RecalculateBounds();

        return mesh2;
    }


    Mesh Build()
    {
        var mesh = new Mesh();

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();
        var triangles = new List<int>();

        float top = height * 1.0f;
        //float bottom = -height * 0.5f;
        float bottom = 0.0f;
        GenerateCap(segments + 1, top, bottom, radius, vertices, uvs, normals, true);


        var len = (segments + 1) * 2;

        for (int i = 0; i < segments + 1; i++)
        {
            int idx = i * 2;
            int a = idx, b = idx + 1, c = (idx + 2) % len, d = (idx + 3) % len;
            triangles.Add(a);
            triangles.Add(c);
            triangles.Add(b);

            triangles.Add(d);
            triangles.Add(b);
            triangles.Add(c);
        }

        if (openEnded)
        {
            GenerateCap(segments + 1, top, bottom, radius, vertices, uvs, normals, false);



            vertices.Add(new Vector3(center.x, center.y, top));
            uvs.Add(new Vector2(0.5f, 1f));
            normals.Add(new Vector3(0f, 0f, 1f));

            vertices.Add(center); // bottom
            uvs.Add(new Vector2(0.5f, 0f));
            normals.Add(new Vector3(0f, 0f, -1f));

            var it = vertices.Count - 2;
            var ib = vertices.Count - 1;

            var offset = len;

            //upper
            for (int i = 0; i < len; i += 2)
            {
                triangles.Add(it);
                triangles.Add((i + 2) % len + offset);
                triangles.Add(i + offset);
            }

            //down
            for (int i = 1; i < len; i += 2)
            {
                triangles.Add(ib);
                triangles.Add(i + offset);
                triangles.Add((i + 2) % len + offset);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateBounds();
        //    mesh.transform.position = extrude.center;
        //pos.transform.position = extrude.center;
        return mesh;
    }

    void GenerateCap(int segments, float top, float bottom, float radius, List<Vector3> vertices, List<Vector2> uvs, List<Vector3> normals, bool side)
    {
        for (int i = 0; i < segments; i++)
        {
            // 0.0 ~ 1.0
            float ratio = (float)i / (segments - 1);

            // 0.0 ~ 2π
            float rad = ratio * PI2;

            float cos = Mathf.Cos(rad), sin = Mathf.Sin(rad);
            float x = cos * radius+center.x, y = sin * radius+center.y;
            Vector3 tp = new Vector3(x, y, top), bp = new Vector3(x, y, bottom);

            // top
            vertices.Add(tp);
            uvs.Add(new Vector2(ratio, 1f));

            // bottom
            vertices.Add(bp);
            uvs.Add(new Vector2(ratio, 0f));

            if (side)
            {
                var normal = new Vector3(cos, sin, 0f);
                normals.Add(normal);
                normals.Add(normal);
            }
            else
            {
                normals.Add(new Vector3(0f, 0f, -1f));
                normals.Add(new Vector3(0f, 0f, -1f));
            }
        }

    }




}
