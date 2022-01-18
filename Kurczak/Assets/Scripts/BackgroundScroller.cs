using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1f, 1f)] public float scrollSpeed = 0.5f;
    private float offset = 0;
    //private Material _material;

    //test
    public int materialIndex = 0;
    Renderer _renderer;
    public List<Material> _material = new List<Material>();
    [SerializeField] float timeOffset;
    public float rotationSpeed = 0.1f;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _material[materialIndex] = _renderer.material;
        //_material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        foreach(Material m in _material)
        {
            m.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
        timeOffset += Time.deltaTime;
        if (timeOffset > 1)
        {
            timeOffset = 0;
            NextTexture();

        }
        /*        offset = Time.time * (rotationSpeed / 1000);
                _renderer.material.mainTextureOffset = new Vector2(0, offset);
                NextTexture();*/
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

    //Update//

    //public List<Material> _material = new List<Material>();
    //Renderer _renderer;
    //public float rotationSpeed = 10f;
    //public int materialIndex = 0;
    //[SerializeField] float offset;


    //private void Start()
    //{
    //    _renderer = GetComponent<Renderer>();
    //    _material[materialIndex] = _renderer.material;
    //    gameObject.renderer.material = _material[materialIndex]; 
    //}

    //private void Update()
    //{
    //    offset = Time.deltaTime * rotationSpeed;
    //    if (offset > 1)
    //    {
    //        NextTexture();
    //        offset = 0;
    //    }
    //    /*        offset = Time.time * (rotationSpeed / 1000);
    //            _renderer.material.mainTextureOffset = new Vector2(0, offset);
    //            NextTexture();*/
    //}

    void NextTexture()
    {
        if (materialIndex == (_material.Count - 1))
        {
            materialIndex = 0;
        }
        else
        {
            materialIndex++;
        }

        _renderer.material = _material[materialIndex];
    }
}
