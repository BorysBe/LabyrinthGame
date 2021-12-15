using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHandler : MonoBehaviour
{
    public int xSize = 20;
    public int zSize = 20;

    public Material material;
    void Start()
    {

        Vector3[]vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        Vector2[] uv = new Vector2[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                //float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;  // linie 35 i 36 odpowiadaj¹ za pofa³dowanie terenu
                //vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        int[] triangles = new int[xSize * zSize * 6];

        int vertexCounter = 0;
        int triangeCounter = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[triangeCounter + 0] = vertexCounter + 0;
                triangles[triangeCounter + 1] = vertexCounter + xSize + 1;
                triangles[triangeCounter + 2] = vertexCounter + 1;
                triangles[triangeCounter + 3] = vertexCounter + 1;
                triangles[triangeCounter + 4] = vertexCounter + xSize + 1;
                triangles[triangeCounter + 5] = vertexCounter + xSize + 2;
                //uv[triangeCounter + 0] = new Vector2(0, 0);
                //uv[triangeCounter + 1] = new Vector2(1, 0);
                //uv[triangeCounter + 2] = new Vector2(0, 1);
                //uv[triangeCounter + 3] = new Vector2(0, 1);
                //uv[triangeCounter + 4] = new Vector2(1, 0);
                //uv[triangeCounter + 5] = new Vector2(1, 1);

                vertexCounter++;
                triangeCounter += 6;

            }
            vertexCounter++;
        }

        //    Vector3[] vertices = new Vector3[4];
        //Vector2[] uv = new Vector2[4];
        //int[] triangles = new int[6];

        //vertices[0] = new Vector3(0, 0, 0);
        //vertices[1] = new Vector3(0, 0, 1);
        //vertices[2] = new Vector3(1, 0, 0);
        //vertices[3] = new Vector3(1, 0, 1);

        //uv[0] = new Vector2(0, 1);
        //uv[1] = new Vector2(1, 1);
        //uv[2] = new Vector2(0, 0);
        //uv[3] = new Vector2(1, 0);

        //triangles[0] = 0;
        //triangles[1] = 1;
        //triangles[2] = 2;
        //triangles[3] = 2;
        //triangles[4] = 1;
        //triangles[5] = 3;

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        gameObject.transform.localScale = new Vector3(30, 1, 30);

        gameObject.GetComponent<MeshFilter>().mesh = mesh;

        gameObject.GetComponent<MeshRenderer>().material = material;
    }
}
