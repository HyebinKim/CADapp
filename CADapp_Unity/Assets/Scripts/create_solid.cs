using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class create_solid : MonoBehaviour
{
    MeshFilter filter;
    new MeshRenderer renderer;

    //setting


     
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
         



    //call mainUI
    Main_code feature_info;
    Touch_main touch_info;

    //parameter
    public float length=0.0f;
    float length_save = 0.0f;
    public int direct = 1;

    public Text text_length;

    void Start()
    {
        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
        touch_info = GameObject.Find("Touch").GetComponent<Touch_main>();

        length_save = 0.0f;
        length = 0.0f;
        text_length.text = "";
    }

    void Update()
    {

        if (feature_info.m_mode == 3)
        {
            switch (feature_info.s_mode)
            {
                case 0:
                    text_length.text = "";
                    break;
                case 1: //extrusion

                    length = length_save + touch_info.length_v;
                    text_length.text = "Length= " + Mathf.Round(length*10) * 0.1f;

                    if (length >= 0)
                    {
                        direct = 1;
                    }
                    else
                    {
                        direct = -1;
                    }

                    Filter.sharedMesh = Build(0); //mesh 반환

                    if (touch_info.touch1 == 1)
                    {
                        length_save = length;
                    }

                    break;
                case 2: //cut extrusion
                    text_length.text = "Length=";



                    break;
                default:
                    text_length.text = "";
                    break;
            }

        }
        else
        {
            text_length.text = "";
        }
    }

    Mesh Build(int parent)
    {
        var mesh = new Mesh();

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();
        var triangles = new List<int>();

        //create top, bottom face: GenerateCap
        GenerateCap(vertices, uvs, normals, triangles);
        GenerateSide(vertices, uvs, normals, triangles);

        //making mesh

        //total mesh

        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateBounds();

        return mesh;
    }

    void GenerateCap(List<Vector3> vertices, List<Vector2> uvs, List<Vector3> normals, List<int> triangles)
    {
        if (feature_info.s_feature == 1)//rectangle
        {
            
            Vector3 center = (feature_info.rec[0] + feature_info.rec[2]) / 2;


            //bottom
            for(int i = 0; i < 4; i++)
            {
                vertices.Add(feature_info.rec[i]);
                normals.Add( -direct * feature_info.rec_plane.normal);

            }
            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 1.0f));
            uvs.Add(new Vector2(0.0f, 1.0f));

           

            //top
            for (int i = 0; i < 4; i++)
            {
                vertices.Add(feature_info.rec[i]+length * feature_info.rec_plane.normal);
                normals.Add(direct * feature_info.rec_plane.normal);

            }

            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 1.0f));
            uvs.Add(new Vector2(0.0f, 1.0f));

            vertices.Add(center);
            uvs.Add(new Vector2(0.5f, 0.5f));
            normals.Add(-direct * feature_info.rec_plane.normal);

            vertices.Add(center + length  * feature_info.rec_plane.normal);
            uvs.Add(new Vector2(0.5f, 0.5f));
            normals.Add(direct * feature_info.rec_plane.normal);

            //direction: 1-positive, -1 -negative
            if (direct == 1)
            {
                //bottom
                int j = 0;
                for (int i = 0; i < 4; i++)
                {
                    triangles.Add(8 + 1 * j);
                    triangles.Add(i + 4 * j);

                    if (i != 3)
                    {
                        triangles.Add(i + 1 + 4 * j);
                    }
                    else
                    {
                        triangles.Add(0 + 4 * j);
                    }
                }

                //top
                j = 1;
                for (int i = 0; i < 4; i++)
                {
                    triangles.Add(8 + 1 * j);

                    if (i != 3)
                    {
                        triangles.Add(i + 1 + 4 * j);
                    }
                    else
                    {
                        triangles.Add(0 + 4 * j);
                    }

                    triangles.Add(i + 4 * j);

                }
            }
            else
            {
                //bottom
                int j = 0;
                for (int i = 0; i < 4; i++)
                {
                    triangles.Add(i + 4 * j);
                    triangles.Add(8 + 1 * j);
                    

                    if (i != 3)
                    {
                        triangles.Add(i + 1 + 4 * j);
                    }
                    else
                    {
                        triangles.Add(0 + 4 * j);
                    }
                }

                //top
                j = 1;
                for (int i = 0; i < 4; i++)
                {
                    triangles.Add(8 + 1 * j);
                    triangles.Add(i + 4 * j);

                    if (i != 3)
                    {
                        triangles.Add(i + 1 + 4 * j);
                    }
                    else
                    {
                        triangles.Add(0 + 4 * j);
                    }

                    

                }
            }


        }
        else if (feature_info.s_feature == 2)
        {
            int segments = 60;
            Vector3 temp;

            float angle = 0.0f;

            //bottom
            for (int i = 0; i < (segments); i++)
            {
                temp = feature_info.cir.center + Mathf.Cos(Mathf.Deg2Rad * angle) * feature_info.cir.radius.x * feature_info.cir.cir_plane.u + Mathf.Sin(Mathf.Deg2Rad * angle) * feature_info.cir.radius.y * feature_info.cir.cir_plane.v;
                vertices.Add(temp);
                uvs.Add(new Vector2((float)i / (segments - 1), 0.0f));

                //normals.Add(new Vector3(0,0,1));
                normals.Add(-direct * feature_info.cir.cir_plane.normal);

                angle += (360f / segments);
            }
            //top
            angle = 0.0f;
            for (int i = 0; i < (segments); i++)
            {
                temp = feature_info.cir.center + Mathf.Cos(Mathf.Deg2Rad * angle) * feature_info.cir.radius.x * feature_info.cir.cir_plane.u + Mathf.Sin(Mathf.Deg2Rad * angle) * feature_info.cir.radius.y * feature_info.cir.cir_plane.v;
                temp = temp + +length  * feature_info.cir.cir_plane.normal;
                vertices.Add(temp);
                uvs.Add(new Vector2((float)i / (segments - 1), 1.0f));
                normals.Add(direct * feature_info.cir.cir_plane.normal);

                angle += (360f / segments);
            }

            vertices.Add(feature_info.cir.center);
            uvs.Add(new Vector2(0.5f, 0.5f));
            //normals.Add(new Vector3(0, 0, 1));
            normals.Add(-direct * feature_info.cir.cir_plane.normal);

            vertices.Add(feature_info.cir.center + length  * feature_info.cir.cir_plane.normal);
            uvs.Add(new Vector2(0.5f, 0.5f));
            normals.Add(direct * feature_info.cir.cir_plane.normal);

            //direction: 1-positive, -1 -negative
            if (direct == 1)
            {
                for (int i = 0; i < segments; i++)
                {
                    triangles.Add(2 * segments);
                    if (i + 1 != segments)
                    {
                        triangles.Add(i + 1);
                    }
                    else
                    {
                        triangles.Add(0);
                    }
                    triangles.Add(i);
                }

                for (int i = 0; i < segments; i++)
                {
                    triangles.Add(2 * segments + 1);
                    triangles.Add(segments + i);

                    if (i + 1 != segments)
                    {
                        triangles.Add(segments + i + 1);
                    }
                    else
                    {
                        triangles.Add(segments);
                    }



                }
            }
            else
            {
                for (int i = 0; i < segments; i++)
                {
                    triangles.Add(2 * segments);
                    triangles.Add(i);
                    if (i + 1 != segments)
                    {
                        triangles.Add(i + 1);
                    }
                    else
                    {
                        triangles.Add(0);
                    }
                    
                }

                for (int i = 0; i < segments; i++)
                {
                    triangles.Add(segments + i);
                    triangles.Add(2 * segments + 1);
                    

                    if (i + 1 != segments)
                    {
                        triangles.Add(segments + i + 1);
                    }
                    else
                    {
                        triangles.Add(segments);
                    }



                }
            }


        }
    }

    void GenerateSide(List<Vector3> vertices, List<Vector2> uvs, List<Vector3> normals, List<int> triangles)
    {
        if (feature_info.s_feature == 1)//rectangle
        {
            for(int i = 0; i < 4; i++)
            {
                if (i != 3)
                {
                    vertices.Add(vertices[i]);
                    vertices.Add(vertices[i + 1]);
                    vertices.Add(vertices[i + 4]);
                    vertices.Add(vertices[i + 1 + 4]);

                    uvs.Add(new Vector2(0.0f, 0.0f));
                    uvs.Add(new Vector2(1.0f, 0.0f));
                    uvs.Add(new Vector2(1.0f, 1.0f));
                    uvs.Add(new Vector2(0.0f, 1.0f));

                    Vector3 tot_normal = Vector3.Cross(vertices[i + 1] - vertices[i], vertices[i + 4] - vertices[i]);
                    normals.Add(tot_normal);
                    normals.Add(tot_normal);
                    normals.Add(tot_normal);
                    normals.Add(tot_normal);

                    //direction: 1-positive, -1 -negative
                    if (direct == 1)
                    {
                        triangles.Add(10 + i * 4);
                        triangles.Add(10 + i * 4 + 2);
                        triangles.Add(10 + i * 4 + 1);


                        triangles.Add(10 + i * 4 + 2);
                        triangles.Add(10 + i * 4 + 3);
                        triangles.Add(10 + i * 4 + 1);
                    }
                    else
                    {
                        triangles.Add(10 + i * 4);
                        triangles.Add(10 + i * 4 + 1);
                        triangles.Add(10 + i * 4 + 2);


                        triangles.Add(10 + i * 4 + 2);
                        triangles.Add(10 + i * 4 + 1);
                        triangles.Add(10 + i * 4 + 3);
                    }


                }

                else
                {
                    vertices.Add(vertices[i]);
                    vertices.Add(vertices[0]);
                    vertices.Add(vertices[i + 4]);
                    vertices.Add(vertices[0+ 4]);

                    uvs.Add(new Vector2(0.0f, 0.0f));
                    uvs.Add(new Vector2(1.0f, 0.0f));
                    uvs.Add(new Vector2(1.0f, 1.0f));
                    uvs.Add(new Vector2(0.0f, 1.0f));

                    Vector3 tot_normal = Vector3.Cross(vertices[0] - vertices[i], vertices[i + 4] - vertices[i]);
                    normals.Add(tot_normal);
                    normals.Add(tot_normal);
                    normals.Add(tot_normal);
                    normals.Add(tot_normal);

                    //direction: 1-positive, -1 -negative
                    if (direct == 1)
                    {
                        triangles.Add(10 + i * 4);
                        triangles.Add(10 + i * 4 + 2);
                        triangles.Add(10 + i * 4 + 1);


                        triangles.Add(10 + i * 4 + 2);
                        triangles.Add(10 + i * 4 + 3);
                        triangles.Add(10 + i * 4 + 1);
                    }
                    else
                    {
                        triangles.Add(10 + i * 4);
                        
                        triangles.Add(10 + i * 4 + 1);
                        triangles.Add(10 + i * 4 + 2);


                        triangles.Add(10 + i * 4 + 2);
                        
                        triangles.Add(10 + i * 4 + 1);
                        triangles.Add(10 + i * 4 + 3);
                    }


                }

            }


        }
        else if (feature_info.s_feature == 2)
        {
            int segments = 60;
            Vector3 temp;

            float angle = 0.0f;

            //bottom
            for (int i = 0; i < (segments); i++)
            {
                temp = feature_info.cir.center + Mathf.Cos(Mathf.Deg2Rad * angle) * feature_info.cir.radius.x * feature_info.cir.cir_plane.u + Mathf.Sin(Mathf.Deg2Rad * angle) * feature_info.cir.radius.y * feature_info.cir.cir_plane.v;
                vertices.Add(temp);
                uvs.Add(new Vector2((float)i / (segments - 1), 0.0f));
                normals.Add(Mathf.Cos(Mathf.Deg2Rad * angle) * feature_info.cir.cir_plane.u + Mathf.Sin(Mathf.Deg2Rad * angle) * feature_info.cir.cir_plane.v);

                angle += (360f / segments);
            }
            //top
            angle = 0.0f;
            for (int i = 0; i < (segments); i++)
            {
                temp = feature_info.cir.center + Mathf.Cos(Mathf.Deg2Rad * angle) * feature_info.cir.radius.x * feature_info.cir.cir_plane.u + Mathf.Sin(Mathf.Deg2Rad * angle) * feature_info.cir.radius.y * feature_info.cir.cir_plane.v;
                temp = temp + +length * feature_info.cir.cir_plane.normal;
                vertices.Add(temp);
                uvs.Add(new Vector2((float)i / (segments - 1), 1.0f));
                normals.Add(Mathf.Cos(Mathf.Deg2Rad * angle) * feature_info.cir.cir_plane.u + Mathf.Sin(Mathf.Deg2Rad * angle) * feature_info.cir.cir_plane.v);

                angle += (360f / segments);
            }

            //direction: 1-positive, -1 -negative
            if (direct == 1)
            {
                for (int i = 0; i < (segments); i++)
                {
                    triangles.Add(i +2*(segments+1));
                    if (i + 1 != segments)
                    {
                        triangles.Add(i + 1 + 2 * (segments + 1));
                    }
                    else
                    {
                        triangles.Add(0 + 2 * (segments + 1));
                    }
                    triangles.Add(i + segments + 2 * (segments + 1));

                    if (i + 1 != segments)
                    {
                        triangles.Add(i + 1 + 2 * (segments + 1));
                        triangles.Add(i + 1 + segments + 2 * (segments + 1));
                    }
                    else
                    {
                        triangles.Add(0 + 2 * (segments + 1));
                        triangles.Add(0 + segments + 2 * (segments + 1));
                    }

                    triangles.Add(i + segments + 2 * (segments + 1));
                }
            }
            else
            {
                for (int i = 0; i < (segments); i++)
                {
                    
                    if (i + 1 != segments)
                    {
                        triangles.Add(i + 1 + 2 * (segments + 1));
                    }
                    else
                    {
                        triangles.Add(0 + 2 * (segments + 1));
                    }
                    triangles.Add(i + 2 * (segments + 1));
                    triangles.Add(i + segments + 2 * (segments + 1));

                    if (i + 1 != segments)
                    {
                       
                        triangles.Add(i + 1 + segments + 2 * (segments + 1));
                        triangles.Add(i + 1 + 2 * (segments + 1));
                    }
                    else
                    {
                       
                        triangles.Add(0 + segments + 2 * (segments + 1));
                        triangles.Add(0 + 2 * (segments + 1));
                    }

                    triangles.Add(i + segments + 2 * (segments + 1));
                }
            }


        }
    }

 

}
