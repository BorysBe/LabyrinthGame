using UnityEngine;

public class DrawTexture : MonoBehaviour
{
    // Draws a texture on the screen at 10, 10 with 100 width, 100 height.

    public Texture aTexture;

    private void Start()
    {
        OnGUI();
    }

    void OnGUI()
    {
        if (Event.current.type.Equals(EventType.Repaint))
        {
            GUI.DrawTexture(new Rect(10, 10, 60, 60), aTexture, ScaleMode.ScaleToFit, true, 10.0F);
        }
    }
}
