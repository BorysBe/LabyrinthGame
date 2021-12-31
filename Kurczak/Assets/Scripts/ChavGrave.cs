using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChavGrave : MonoBehaviour
{

    public List<GameObject> _sprites = new List<GameObject>();
    [SerializeField] float spriteChangeDelay = 1f;
    [SerializeField] float destroyDelay = 5f;
    [SerializeField] float speedController = 1;
    [SerializeField] [Range(0, 1)] float opacityChangedInTime = 1f;
    [SerializeField] float timeDelay = 0.5f;
    float timer = 0;
    int sceneIndex = 0;
    public float positionY = 0;
    float opacityValue = 1f;


    public void Start()
    {
        foreach (GameObject s in _sprites)
        {
            s.SetActive(false);
            Destroy(s, destroyDelay);
        }
        Destroy(this.gameObject, destroyDelay);
    }

    public void Update()
    {
        if (timeDelay >= 0)
        {
            timeDelay -= Time.deltaTime;
            return;
        }
        else
        {
            SpriteChanger();
            Move();
            opacityValue -= opacityChangedInTime * Time.deltaTime;
            OpacityChanger();
        }
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

    void Move()
    {
        positionY = speedController * Time.deltaTime;
        this.transform.Translate(0, positionY, 0);
    }

    void OpacityChanger()
    {
        foreach (GameObject s in _sprites)
        {
            s.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, opacityValue);
        }
    }
}
