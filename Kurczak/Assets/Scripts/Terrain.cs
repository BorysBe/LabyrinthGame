using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    //private BackgroundScroller _backgroundScroller;

    public List<GameObject> _sprites = new List<GameObject>();
    [SerializeField] float spriteChangeDelay = 1f;
    float timer = 0;
    int sceneIndex = 0;
    //public List<Material> _material = new List<Material>();

    public void Start()
    {
        foreach (GameObject s in _sprites)
        {
            s.SetActive(false);
        }
        _sprites[0].SetActive(true);
    }

    public void Update()
    {
        SpriteChanger();
    }

    private void SpriteChanger()
    {
        timer += Time.deltaTime * 10;
        if (timer >= spriteChangeDelay)
        {
            timer = 0;
            _sprites[sceneIndex].SetActive(false);
            if (sceneIndex == _sprites.Count - 1)
            {
                sceneIndex = 0;
            }
            else
            {
                sceneIndex++;
            }
            _sprites[sceneIndex].SetActive(true);
        }
    }
}
