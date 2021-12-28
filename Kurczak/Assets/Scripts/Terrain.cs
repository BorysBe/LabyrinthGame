using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour, ITerrain
{
    private BackgroundScroller _backgroundScroller;

    public void Start()
    {
        _backgroundScroller = GetComponent<BackgroundScroller>();
    }

    public void Move(float value)
    {
        _backgroundScroller.OffsetUpdate(value);
    }

    public void MoveRight(float value)
    {

    }

}
