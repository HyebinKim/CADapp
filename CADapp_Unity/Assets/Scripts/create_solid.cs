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
    //public int direct = 1;

    public Text text_length;

    //feature info
    solid_def temp;

    public Text info;


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
        //var mesh = new Mesh(); //rendering 할꺼.
        temp = new solid_def();

        if (feature_info.m_mode == 3)
        {
            if (feature_info.line_collect.Count > 0)
            {
                feature_info.selected_line_num = 0; //첫번째 라인 선택.
            }

            switch (feature_info.s_mode)
            {
                case 0:
                    text_length.text = "";
                    break;
                case 1: //extrusion

                    info.text = "extrusion";

                    //input: parent, direct, length

                    if (feature_info.line_collect.Count > 0) //수정 필요.
                    {
                        //length value
                        length = length_save + touch_info.length_v;
                        text_length.text = "Length= " + Mathf.Round(length * 10) * 0.1f;

                        if (touch_info.touch1 == 1)
                        {
                            length_save = length;
                        }

                        //mesh generation
                        temp.type = 0;
                        temp.parent = feature_info.line_collect[0];  //수정 필요.

                        Filter.sharedMesh = Build(temp.parent, length, temp.faces); 
                        temp.mesh= Build(temp.parent, length, temp.faces);

                        info.text = "v:" + temp.mesh.vertices.Length + " uv:" + temp.mesh.uv.Length + "  n:" + temp.mesh.normals.Length + "  t:" + temp.mesh.triangles.Length;
                    }

                    break;
                case 2: //cut extrusion
                    text_length.text = "Length=";

                    if (feature_info.solid_collect.Count > 0 && feature_info.line_collect.Count > 0) //수정 필요.
                    {
                        temp.type = 1;

                    }
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


    Mesh Build(line_def parent, float length, List<face_def> faces)
    {
        var mesh = new Mesh(); // total mesh;

        var vertices = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();
        var triangles = new List<int>();

        top_bottom(parent, length, vertices, uvs, normals, triangles, faces);
        //side(parent, length, vertices, uvs, normals, triangles, faces);

        //create top, bottom face: GenerateCap
        //GenerateCap(vertices, uvs, normals, triangles);

        // GenerateSide(vertices, uvs, normals, triangles);

        //making mesh

        //total mesh

        info.text="v:"+vertices.Count+" uv:"+uvs.Count+"  n:"+normals.Count+"  t:"+triangles.Count;

        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateBounds();

        return mesh;
    }

    void bottom(line_def sketch, float length, List<Vector3> vertices, List<Vector2> uvs, List<Vector3> normals, List<int> triangles, List<face_def> faces)
    {
        Vector3[] pos = calculate_position(sketch);

        vertices.Add(sketch.center);
        uvs.Add(new Vector2(0.5f, 0.5f));
        normals.Add(-sketch.plane.normal);

        for (int i = 0; i < 4; i++)
        {
            vertices.Add(pos[i]);
            normals.Add(-sketch.plane.normal);
        }
        uvs.Add(new Vector2(0.0f, 0.0f));
        uvs.Add(new Vector2(1.0f, 0.0f));
        uvs.Add(new Vector2(1.0f, 1.0f));
        uvs.Add(new Vector2(0.0f, 1.0f));

        triangles.Add(0); triangles.Add(1); triangles.Add(2);
        triangles.Add(0); triangles.Add(2); triangles.Add(3); 
        triangles.Add(0); triangles.Add(3); triangles.Add(4); 
        triangles.Add(0); triangles.Add(4); triangles.Add(1); 

        return;
    }

    void top_bottom(line_def sketch, float length, List<Vector3> vertices, List<Vector2> uvs, List<Vector3> normals, List<int> triangles, List<face_def> faces)
    {
        int current = 0;

        //vertices and uv.
        Vector3[] pos = calculate_position(sketch);
        int direct = 1; //positive
        if (length < 0) direct = -1; //negative

        //bottom

        vertices.Add(sketch.center);
        uvs.Add(new Vector2(0.5f, 0.5f));
        normals.Add(-direct* sketch.plane.normal);
        //normals.Add(-sketch.plane.normal);

        for (int i = 0; i < pos.Length; i++)
        {
            vertices.Add(pos[i]);
            uvs.Add(new Vector2(0.0f, 0.0f));
            normals.Add(-direct * sketch.plane.normal);
            //normals.Add(-sketch.plane.normal);

        }

        

        // face_def bottom = new face_def();
        // bottom.plane = sketch.plane;
        //  bottom.plane.normal *= -direct;

        for (int i = 0; i < pos.Length; i++)
        {
            if (direct == 1)
            {
                triangles.Add(0); //bottom center
                //bottom.triangles.Add(0);

                triangles.Add(i + 1);
                //bottom.triangles.Add(i+1);

                if (i + 1 == pos.Length)
                {
                    triangles.Add(0 + 1);
                    //bottom.triangles.Add(0 + 1);
                }
                else
                {
                    triangles.Add(i + 2);
                    //bottom.triangles.Add(i + 2);
                }
            }
            else
            {
                triangles.Add(0); //bottom center
                //bottom.triangles.Add(0);

                if (i + 1 == pos.Length)
                {
                    triangles.Add(0 + 1);
                    //bottom.triangles.Add(0 + 1);
                }
                else
                {
                    triangles.Add(i + 2);
                   // bottom.triangles.Add(i + 2);
                }

                triangles.Add(i + 1);
                //bottom.triangles.Add(i+1);

            }




        }

        //faces.Add(bottom);

        current = vertices.Count;

        //top
        Vector3 add = sketch.plane.normal * length;
        vertices.Add(sketch.center+add);
        uvs.Add(new Vector2(0.5f, 0.5f));
        normals.Add(direct * sketch.plane.normal);

        for (int i = 0; i < pos.Length; i++)
        {
            vertices.Add(pos[i]+add);
            uvs.Add(new Vector2(0.0f, 0.0f));
            normals.Add(direct * sketch.plane.normal);

        }

        for (int i = 0+ current; i < pos.Length+ current; i++)
        {
            if (direct == 1)
            {
                triangles.Add(current); //top center
                

                if (i + 1 == pos.Length+ current)
                {
                    triangles.Add(current + 1);
                }
                else
                {
                    triangles.Add(i + 2);
                }

                triangles.Add(i + 1);
            }
            else
            {
                triangles.Add(current); //top center
                triangles.Add(i + 1);

                if (i + 1 == pos.Length+ current)
                {
                    triangles.Add(current + 1);
                }
                else
                {
                    triangles.Add(i + 2);
                }

            }
        }

        return;
    }

    void side(line_def sketch, float length, List<Vector3> vertices, List<Vector2> uvs, List<Vector3> normals, List<int> triangles)
    {
        Vector3[] pos = calculate_position(sketch);
        int direct = 1; //positive
        if (length < 0) direct = -1; //negative

        int current = vertices.Count; //until top, bottom
        Vector3 add = sketch.plane.normal * length ;

    
        for (int i = 0; i < pos.Length; i++)
        {
           
            //normals
            Vector3 tot_normal;
            if (i + 1 == pos.Length)
            {
                tot_normal = Vector3.Normalize(Vector3.Cross((pos[i] + add) - pos[i], pos[0] - pos[i]));
               
            }
            else
            {
                tot_normal = Vector3.Normalize(Vector3.Cross((pos[i] + add) - pos[i], pos[i + 1] - pos[i]));
              
            }


            //extrusion direction
            if (direct == 1)
            {
                //vertices
                vertices.Add(pos[i]);
                if (i + 1 == pos.Length)
                {
                    vertices.Add(pos[0]);
                    vertices.Add(pos[0] + add);
                }
                else
                {
                    vertices.Add(pos[i + 1]);
                    vertices.Add(pos[i + 1] + add);
                }
                vertices.Add(pos[i]+add);

            }
            else
            {
                //vertices
                vertices.Add(pos[i]);
                vertices.Add(pos[i] + add);

                if (i + 1 == pos.Length)
                {
                    vertices.Add(pos[0] + add);
                    vertices.Add(pos[0]);
                    
                }
                else
                {
                    vertices.Add(pos[i + 1] + add);
                    vertices.Add(pos[i + 1]);
                    
                }

                //normal change
                tot_normal = -tot_normal;


            }

            //uvs
            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 0.0f));
            uvs.Add(new Vector2(1.0f, 1.0f));
            uvs.Add(new Vector2(0.0f, 1.0f));

            //normals
            normals.Add(tot_normal);
            normals.Add(tot_normal);
            normals.Add(tot_normal);
            normals.Add(tot_normal);

            //side mesh and triangles
           
            triangles.Add(current +i*4);
            triangles.Add(current +1 + i * 4);
            triangles.Add(current + 3 + i * 4);

            triangles.Add(current + 1 + i * 4);
            triangles.Add(current + 2 + i * 4);
            triangles.Add(current + 3 + i * 4);

        }

        return;
    }

    /*
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

            //

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
         
         */

    Vector3[] calculate_position(line_def sketch)
    {
      
        Vector3[] pos;
        if (sketch.type == 0) //rectangle
        {
            pos = new Vector3[4];

            pos[0] = sketch.center - sketch.plane.u * sketch.radius.x + sketch.plane.v * sketch.radius.y;
            pos[1] = sketch.center - sketch.plane.u * sketch.radius.x - sketch.plane.v * sketch.radius.y;
            pos[2] = sketch.center + sketch.plane.u * sketch.radius.x - sketch.plane.v * sketch.radius.y;
            pos[3] = sketch.center + sketch.plane.u * sketch.radius.x + sketch.plane.v * sketch.radius.y;

        }
        else //circle: sketch.type == 1
        {
            int segments = 60;
            pos = new Vector3[segments];

            Vector3 temp;

            float angle = 0.0f;

            for (int i = 0; i < (segments); i++)
            {
                temp = sketch.center + Mathf.Cos(Mathf.Deg2Rad * angle) * sketch.radius.x * sketch.plane.u + Mathf.Sin(Mathf.Deg2Rad * angle) * sketch.radius.y * sketch.plane.v;
                pos[segments-i-1] = temp;

                angle += (360f / segments);
            }
        }

        return pos;
    }



    public void solid_export()
    {
        if (feature_info.m_mode == 3 && feature_info.s_mode != 0) //not complete
        {
            if (feature_info.s_mode == 1)
            {
                temp.name = "extrusion" + (feature_info.solid_collect.Count + 1);
            }
            feature_info.solid_collect.Add(temp);
        }

    }

}
