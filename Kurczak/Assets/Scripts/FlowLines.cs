using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowLines : MonoBehaviour
{
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        RenderLines(new Vector3[0]);
    }

    void RenderLines(params Vector3[] points)
    {
        GL.Begin(GL.LINES);
        material.SetPass(0);
        GL.Color(material.color);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(1, 1, 1);
        GL.End();
    }
}
