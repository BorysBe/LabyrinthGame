using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AlphaTransparencyButton : MonoBehaviour
{
    public Image img;

    void Start()
    {
        img.alphaHitTestMinimumThreshold = 0.5f;
    }

}
