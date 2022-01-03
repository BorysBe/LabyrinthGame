using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaTransparencyButton : MonoBehaviour
{
    public Image[] img;

    void Start()
    {
        for(int i = 0; i < img.Length; i ++)
        img[i].alphaHitTestMinimumThreshold = 0.5f;
    }

}
