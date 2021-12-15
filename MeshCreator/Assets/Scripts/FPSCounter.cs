using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public int avgFramerate;
    public Text fpsDisplay;

    void Update()
    {
        float fps = 0;
        fps = (int)(1 / Time.unscaledDeltaTime);
        avgFramerate = (int)fps;
        fpsDisplay.text = avgFramerate.ToString() + " FPS";
    }
}
