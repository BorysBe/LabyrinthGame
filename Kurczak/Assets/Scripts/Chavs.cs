using System;
using System.Collections.Generic;
using UnityEngine;

public class Chavs : MonoBehaviour
{
    public List<Sprite> _sprites = new List<Sprite>();

    public Action ActiveDrawing { get; private set; }
    public Action ActiveSound { get; private set; }

}
