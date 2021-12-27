using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    private BackgroundScroller _backgroundScroller;
    private Transform _parentTransform;
    public float backgroundWidth;
    int slidingCounter = 0;

    void Start()
    {
        _backgroundScroller = GetComponentInParent<BackgroundScroller>();
        _parentTransform = GetComponentInParent<Transform>();
        backgroundWidth = _parentTransform.lossyScale.x;
        Debug.Log(backgroundWidth);
    }

    void Update()
    {
        transform.position = new Vector3(10 * backgroundWidth * ((-_backgroundScroller.GetOffset()) + slidingCounter), 0, 0);
        if (this.transform.position.x > backgroundWidth / 2 * 10)
        {
            transform.position = new Vector3(- backgroundWidth / 2 * 10 + 0.0001f, 0, 0);
            slidingCounter -= 1;
        }
        if (this.transform.position.x < - backgroundWidth / 2 * 10)
        {
            transform.position = new Vector3(backgroundWidth / 2 * 10 - 0.0001f, 0, 0);
            slidingCounter += 1;
        }
    }


}
