using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    /*    [SerializeField] Material[] animationMeshes;
        [SerializeField] float meshChanger = 1;
        MeshRenderer _meshRenderer;

        int animationFrameNumber = 0;

        // Start is called before the first frame update
        void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            animationFrameNumber = (int)Mathf.PingPong(Time.deltaTime * meshChanger, 1);
            _meshRenderer.materials = animationMeshes[animationFrameNumber];
        }*/

    public List<Material> _material = new List<Material>();
    Renderer _renderer;
    public float rotationSpeed = 10f;
    public int materialIndex = 0;
    [SerializeField] float offset;


    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _material[materialIndex] = _renderer.material;
        //gameObject.renderer.material = _material[materialIndex]; 
    }

    private void Update()
    {
        offset = Time.deltaTime * rotationSpeed;
        if(offset > 1)
        {
            NextTexture();
            offset = 0;
        }
/*        offset = Time.time * (rotationSpeed / 1000);
        _renderer.material.mainTextureOffset = new Vector2(0, offset);
        //NextTexture();*/
    }

    void NextTexture()
    {
        if (materialIndex == (_material.Count - 1))
        {
            materialIndex = 0;
        }
        else
        {
            materialIndex ++;
        }

        _renderer.material = _material[materialIndex];
    }
}
