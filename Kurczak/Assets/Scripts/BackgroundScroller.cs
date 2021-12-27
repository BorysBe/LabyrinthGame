using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1f, 1f)] public float scrollSpeed = 0.5f;
    private float offset = 0;
    private Material _material;

    void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        _material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }

    public float OffsetUpdate(float movementInput)
    {
        //offset += 0.5f * movementInput;
        offset += (Time.deltaTime * scrollSpeed) / 10 * movementInput;
        //Debug.Log("offset: " + offset);
        return offset;
    }

    public float GetOffset()
    {
        return offset;
    }
}
