using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsDisplay;
    public Canvas outlineCanvas;

    private void Start()
    {
        outlineCanvas.enabled = false;
    }

    void Update()
    {
        int fps = (int)(1 / Time.unscaledDeltaTime);
        fpsDisplay.text = fps.ToString() + " FPS";

        if(fps <120)
        {
            outlineCanvas.enabled = true;
        }
        else
        {
            outlineCanvas.enabled = false;
        }
    }
}
