using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour, ITerrain
{
    //private BackgroundScroller _backgroundScroller;

    public List<GameObject> _sprites = new List<GameObject>();
    //public List<Material> _material = new List<Material>();

    public void Start()
    {
        //_backgroundScroller = GetComponent<BackgroundScroller>();
    }

    public void Update()
    {
        
    }

    public void Move(float value)
    {
       // _backgroundScroller.OffsetUpdate(value);
    }

    public void MoveRight(float value)
    {

    }

}
