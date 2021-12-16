using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    /*    public TextMeshProUGUI FpsText;

        private float pollingTime = 1f;
        private float time;
        public int frameCount;*/
    public Text fpsDisplay;


    void Update()
    {
        int fps = (int)(1 / Time.unscaledDeltaTime);
        fpsDisplay.text = fps.ToString() + " FPS";

/*        time += Time.deltaTime;

        frameCount++;

        if(time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            FpsText.text = frameRate.ToString() + " FPS";

            time -= pollingTime;
            frameCount = 0;
        }*/
    }
}
