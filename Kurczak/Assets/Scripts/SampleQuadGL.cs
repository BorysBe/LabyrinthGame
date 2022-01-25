using UnityEngine;

public class SampleQuadGL
    : MonoBehaviour
{
    // Draws a Quad in the middle of the screen and
    // Adds the material's Texture to it.

    public Material mat;

    private void Start()
    {
        OnPostRender();
    }
    void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        GL.PushMatrix();
        mat.SetPass(1);
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        GL.TexCoord2(0, 0);
        GL.Vertex3(0.25f, 0.25f, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(0.25f, 0.75f, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(0.75f, 0.75f, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(0.75f, 0.25f, 0);
        GL.End();
        GL.PopMatrix();
    }
}