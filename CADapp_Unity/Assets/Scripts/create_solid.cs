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

    //parameter
    public float length=2.0f;
    public int direct = 1;

    void Start()
    {
        feature_info = GameObject.Find("MainUI").GetComponent<Main_code>();
    }

    void Update()
    {
        if (feature_info.m_mode == 3)
        {
            switch (feature_info.s_mode)
            {
                case 0:
                    break;
                case 1:
                   Filter.sharedMesh = Build();

                    break;
                case 2:
                    break;
                default:
                    break;
            }

        }
    }

    Mesh Build()
    {
        var mesh = new Mesh();

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();
        var triangles = new List<int>();

        //create top, bottom face: GenerateCap
        GenerateCap(vertices, uvs, normals, triangles);
        GenerateSide(vertices, uvs, normals, triangles);

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
                normals.Add( -direct * feature_info.nowP.normal);

            }
            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 1.0f));
            uvs.Add(new Vector2(0.0f, 1.0f));

           

            //top
            for (int i = 0; i < 4; i++)
            {
                vertices.Add(feature_info.rec[i]+length*direct * feature_info.nowP.normal);
                normals.Add(direct * feature_info.nowP.normal);

            }

            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 1.0f));
            uvs.Add(new Vector2(0.0f, 1.0f));

            vertices.Add(center);
            uvs.Add(new Vector2(0.5f, 0.5f));
            normals.Add(-direct * feature_info.nowP.normal);

            vertices.Add(center + length * direct * feature_info.nowP.normal);
            uvs.Add(new Vector2(0.5f, 0.5f));
            normals.Add(direct * feature_info.nowP.normal);



            //bottom
            int j = 0;
            for (int i = 0; i < 4; i++)
            {
                triangles.Add(8 + 1* j);
                triangles.Add(i + 4 * j);

                if (i != 3)
                {
                    triangles.Add(i + 1 + 4* j);
                }
                else
                {
                    triangles.Add(0 + 4* j);
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

                    triangles.Add(10 + i * 4);
                    triangles.Add(10 + i * 4 + 2);
                    triangles.Add(10 + i * 4 + 1);


                    triangles.Add(10 + i * 4 + 2);
                    triangles.Add(10 + i * 4 + 3);
                    triangles.Add(10 + i * 4 + 1);
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

                    triangles.Add(10 + i * 4);
                    triangles.Add(10 + i * 4 + 2);
                    triangles.Add(10 + i * 4 + 1);


                    triangles.Add(10 + i * 4 + 2);
                    triangles.Add(10 + i * 4 + 3);
                    triangles.Add(10 + i * 4 + 1);
                }

            }


        }
    }


 }
