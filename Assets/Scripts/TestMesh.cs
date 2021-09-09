using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMesh : MonoBehaviour
{
    public int pathPoints = 2;
    public Vector3 dirVec = Vector3.forward;

    Vector3[] theBorderLeft;
    Vector3[] theBorderRight;

    public Material mat;

    void Start()
    {
        //theBorderLeft = new Vector3[6];
        //theBorderLeft[0] = new Vector3(0f, 0f, 0f);
        //theBorderLeft[1] = new Vector3(-0.5f, 0f, 0f);
        //theBorderLeft[2] = new Vector3(-0.5f, 0.5f, 0f);
        //theBorderLeft[3] = new Vector3(0f, 0f, 1f);
        //theBorderLeft[4] = new Vector3(-0.5f, 0f, 1f);
        //theBorderLeft[5] = new Vector3(-0.5f, 0.5f, 1f);

        //DefVec();
        DefSideLeft(new Vector3(2f, 2f, 2f));

        for (int i = 0; i < theBorderLeft.Length; i++)
        {
            Debug.Log(i + " ) " + theBorderLeft[i].ToString() + " ... ");
        }

        //TestQuadMesh();
        //GoodTriMesh();
        TriMesh();
        //TestMeshBorder();
    }

    void Update()
    {

    }

    void DefVec()
    {
        theBorderLeft = new Vector3[3*pathPoints];

        theBorderLeft[0] = new Vector3(0f, 0f, 0f);
        theBorderLeft[1] = new Vector3(-0.5f, 0f, 0f);
        theBorderLeft[2] = new Vector3(-0.5f, 0.5f, 0f);
        for (int i = 1; i < pathPoints; i++)
        {
            int idx = 3 * i;
            int pre = 3 * (i - 1);
            
            theBorderLeft[idx + 0] = Vector3.forward + theBorderLeft[pre + 0];
            theBorderLeft[idx + 1] = Vector3.forward + theBorderLeft[pre + 1];
            theBorderLeft[idx + 2] = Vector3.forward + theBorderLeft[pre + 2];
        }
    }

    void DefSideLeft(Vector3 atPoint)
    {
        theBorderLeft = new Vector3[3 * pathPoints];

        theBorderLeft[0] = atPoint + new Vector3(0f, 0f, 0f);
        theBorderLeft[1] = atPoint + new Vector3(-0.5f, 0f, 0f);
        theBorderLeft[2] = atPoint + new Vector3(-0.5f, 0.5f, 0f);
        for (int i = 1; i < pathPoints; i++)
        {
            int idx = 3 * i;
            int pre = 3 * (i - 1);
            for (int j = 0; j < 3; j++) { theBorderLeft[idx + j] = dirVec + theBorderLeft[pre + j]; }
        }
    }

    private void TestQuadMesh()
    {
        float width = 1;
        float height = 1;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = mat;

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0)
        };
        mesh.vertices = vertices;

        int[] tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        mesh.uv = uv;

        meshFilter.mesh = mesh;
    }

    private void TriMesh()   //testing
    {
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = mat;
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        // this array below is the magic formula. do not modify!
        int[] trisBase = new int[18] { 0, 2, 3, 2, 5, 3, 2, 1, 5, 1, 4, 5, 1, 0, 4, 0, 3, 4 };
        int numTris = trisBase.Length;

        // if path is open: shorten the array (last point not included), if path is closed: modular loop over all points (last+1 = 0)
        int loop = pathPoints;
        bool open = true;
        if (open) { numTris = trisBase.Length * (pathPoints - 1); loop -= 1; }
        else      { numTris = trisBase.Length * pathPoints; }

        // vertices defined in DefSideLeft()
        mesh.vertices = theBorderLeft;
        int numVertices = mesh.vertices.Length;

        // defining the triangles
        int[] tris = new int[numTris];
        for (int i = 0; i < loop; i++)
        {
            string tristr = " Triangoli (" + i.ToString() + "): "; // just for debug
            int idx = trisBase.Length * i;
            for (int j = 0; j < trisBase.Length; j++) 
            { 
                tris[idx + j] = trisBase[j] + 3*i;
                if (tris[idx + j] >= numVertices) { tris[idx + j] -= numVertices; }   // modular for closed track
                tristr += tris[idx + j].ToString() + ", "; 
            }
            Debug.Log(tristr);
        }
        mesh.triangles = tris;

        // defining the normals
        Vector3[] normals = new Vector3[numVertices];
        for (int i = 0; i < loop; i++)
        {
            //int idx = 3 * i;
            string tristr = " Normali (" + i.ToString() + "): "; // just for debug
            Debug.Log(tristr);

            normals[3 * i + 0] = Vector3.Cross(Vector3.forward, theBorderLeft[3 * i + 1] - theBorderLeft[3 * i + 0]);
            normals[3 * i + 1] = normals[3 * i + 0];
            normals[3 * i + 2] = Vector3.Cross(Vector3.forward, theBorderLeft[3 * i + 2] - theBorderLeft[3 * i + 1]);
            normals[3 * i + 3] = normals[3 * i + 2];
            normals[3 * i + 4] = Vector3.Cross(Vector3.forward, theBorderLeft[3 * i + 0] - theBorderLeft[3 * i + 2]);
            normals[3 * i + 5] = normals[3 * i + 4];
        }
        mesh.normals = normals;

        //// defining texture mapping
        //Vector2[] uv = new Vector2[numVertices];
        //for (int i = 0; i < pathPoints; i++)
        //{
        //    float ratio = i / (pathPoints); 
        //    uv[3 * i + 0] = new Vector2(3 * i + 0, ratio);
        //    uv[3 * i + 1] = new Vector2(3 * i + 1, ratio);
        //    uv[3 * i + 2] = new Vector2(3 * i + 2, ratio);
        //}
        //mesh.uv = uv;

        meshFilter.mesh = mesh;
    }

    private void GoodTriMesh()   
    {
        int[] trisBase = new int[18] { 0, 2, 3, 2, 5, 3, 2, 1, 5, 1, 4, 5, 1, 0, 4, 0, 3, 4 };

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = mat;
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        // vertices defined in DefSideLeft()
        mesh.vertices = theBorderLeft;

        mesh.triangles = trisBase;

        Vector3[] normals = new Vector3[6]
        {
            Vector3.Cross(Vector3.forward,theBorderLeft[1]-theBorderLeft[0]),
            Vector3.Cross(Vector3.forward,theBorderLeft[1]-theBorderLeft[0]),
            Vector3.Cross(Vector3.forward,theBorderLeft[2]-theBorderLeft[1]),
            Vector3.Cross(Vector3.forward,theBorderLeft[2]-theBorderLeft[1]),
            Vector3.Cross(Vector3.forward,theBorderLeft[0]-theBorderLeft[2]),
            Vector3.Cross(Vector3.forward,theBorderLeft[0]-theBorderLeft[2]),
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[6]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(2, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(2, 1)
        };
        mesh.uv = uv;

        meshFilter.mesh = mesh;
    }


    private void TestMeshBorder()
    {
        //int numTris = 3 * 3 * pathPoints;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = mat;
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        mesh.vertices = theBorderLeft;

        int[] tris = new int[18] { 0, 2, 3, 2, 5, 3, 2, 1, 5, 1, 4, 5, 1, 0, 4, 0, 3, 4 };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[6]
        {
            Vector3.Cross(Vector3.forward,theBorderLeft[1]-theBorderLeft[0]),
            Vector3.Cross(Vector3.forward,theBorderLeft[1]-theBorderLeft[0]),
            Vector3.Cross(Vector3.forward,theBorderLeft[2]-theBorderLeft[1]),
            Vector3.Cross(Vector3.forward,theBorderLeft[2]-theBorderLeft[1]),
            Vector3.Cross(Vector3.forward,theBorderLeft[0]-theBorderLeft[2]),
            Vector3.Cross(Vector3.forward,theBorderLeft[0]-theBorderLeft[2]),
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[6]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(2, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(2, 1)
        };
        mesh.uv = uv;

        meshFilter.mesh = mesh;

        // Custom set of points ...

        //int numTris = 2 * (pathPoints - 1) + 2;
        int numTris = 3 * 6 * (pathPoints - 1);
        int[] sideTrisLeft = new int[numTris];
        int[] sideTrisRight = new int[numTris];

        Debug.Log("Looping over " + pathPoints.ToString() + " ... ");

        float roadWidth = 0.5f;
        float roadThick = 0.01f;
        float barHi = 0.05f; 

        for (int i = 0; i < pathPoints; i++)
        {
            Vector3 pathPoint = theBorderLeft[i];
            Vector3 localUp = Vector3.up;
            Vector3 localRight = Vector3.Cross(localUp, theBorderLeft[i]);

            // Find position of all coordinates for road side mesh
            Vector3 leftA = theBorderLeft[i] - localRight * roadWidth;
            Vector3 leftB = leftA - 0.1f * localRight * roadWidth;
            Vector3 leftC = leftB + barHi * localUp;
            theBorderLeft[3 * i] = leftA;
            theBorderLeft[3 * i + 1] = leftB;
            theBorderLeft[3 * i + 2] = leftC;

            Debug.Log(i.ToString() + ") Left:   A = " + leftA[i].ToString() + " , B = " + leftB[i].ToString() + " , C = " + leftB[i].ToString());
        }

        for (int i = 0; i < pathPoints/3; i++)
        {
            sideTrisLeft[18 * i] = 0;
            sideTrisLeft[18 * i + 1] = 2;
            sideTrisLeft[18 * i + 2] = 3;

            sideTrisLeft[18 * i + 3] = 2;
            sideTrisLeft[18 * i + 4] = 5;
            sideTrisLeft[18 * i + 5] = 3;

            sideTrisLeft[18 * i + 6] = 2;
            sideTrisLeft[18 * i + 7] = 1;
            sideTrisLeft[18 * i + 8] = 5;

            sideTrisLeft[18 * i + 9] = 1;
            sideTrisLeft[18 * i + 10] = 4;
            sideTrisLeft[18 * i + 11] = 5;

            sideTrisLeft[18 * i + 12] = 1;
            sideTrisLeft[18 * i + 13] = 0;
            sideTrisLeft[18 * i + 14] = 4;

            sideTrisLeft[18 * i + 15] = 0;
            sideTrisLeft[18 * i + 16] = 3;
            sideTrisLeft[18 * i + 17] = 4;
        }

        //MeshFilter meshFilter;
        //MeshRenderer meshRenderer;
        Mesh meshLeft = new Mesh();

        meshLeft.Clear();
        meshLeft.vertices = theBorderLeft;
        //mesh.uv = uvs;
        //mesh.normals = normals;
        meshLeft.subMeshCount = 1;
        meshLeft.SetTriangles(sideTrisLeft, 0);
        //mesh.SetTriangles(underRoadTriangles, 1);
        //mesh.SetTriangles(sideOfRoadTriangles, 2);
        meshLeft.RecalculateBounds();

    }


}
