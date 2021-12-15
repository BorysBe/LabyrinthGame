using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall
{
    Mesh _mesh;
    GameObject _gameObject;
}

public class NewMeshGenerator : MonoBehaviour
{
    [SerializeField] [Range(-90, 90)] float xRotation = 0f;
    [SerializeField] [Range(-90, 90)] float yRotation = 0f;
    [SerializeField] [Range(-90, 90)] float zRotation = 0f;

    [SerializeField] float xPosition = 0f;
    [SerializeField] float yPosition = 0f;
    [SerializeField] float zPosition = 0f;
    //[Serializefield] bool _drawBothSides = false;

    public Material material;
    Vector3[] vertices;
    //Vector2[] uvMapping;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    Mesh _mesh;
    GameObject _gameObject;

    void Start()
    {
        _mesh = new Mesh();
        _gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));

        CreateShape();
        UpdateMesh();

        _gameObject.transform.localEulerAngles = new Vector3(zRotation, yRotation, zRotation);
        _gameObject.transform.localPosition = new Vector3(xPosition, yPosition, zPosition);
        //gameObject.transform.localScale = new Vector3(30, 0, 30);
        _gameObject.GetComponent<MeshFilter>().mesh = _mesh;
        _gameObject.GetComponent<MeshRenderer>().material = material;
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        //uvMapping = new Vector2[(xSize + 1) * (zSize + 1)];

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

        triangles = new int[xSize * zSize * 6];

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


    }

    void UpdateMesh()
    {

        _mesh.vertices = vertices;
        //mesh.uv = uvMapping;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }

}
