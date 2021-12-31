using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyExplosion : MonoBehaviour
{

    public List<GameObject> _sprites = new List<GameObject>();
    [SerializeField] float spriteChangeDelay = 1f;
    [SerializeField] float timeToDestroy = .5f;
    float timer = 0;
    int sceneIndex = 0;

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
                Destroy(this.gameObject, timeToDestroy);
            }
            else
            {
                sceneIndex++;
            }
            _sprites[sceneIndex].SetActive(true);
        }
    }
}

