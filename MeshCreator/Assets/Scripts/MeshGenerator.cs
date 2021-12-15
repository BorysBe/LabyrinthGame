using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();

    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for( int x = 0; x <= xSize; x++)
            {
                //vertices[i] = new Vector3(x, 0, z);
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;  // linie 35 i 36 odpowiadaj¹ za pofa³dowanie terenu
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vertexCounter = 0;
        int triangeCounter = 0;

        for(int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[triangeCounter + 0] = vertexCounter + 0;
                triangles[triangeCounter + 1] = vertexCounter + xSize + 1;
                triangles[triangeCounter + 2] = vertexCounter + 1;
                triangles[triangeCounter + 3] = vertexCounter + 1;
                triangles[triangeCounter + 4] = vertexCounter + xSize + 1;
                triangles[triangeCounter + 5] = vertexCounter + xSize + 2;

                vertexCounter++;
                triangeCounter += 6;

            }
            vertexCounter++;
        }


    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

}
