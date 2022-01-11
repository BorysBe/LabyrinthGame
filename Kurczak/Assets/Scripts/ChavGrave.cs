using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChavGrave : MonoBehaviour
{
    List<GameObject> pictures;
    [SerializeField] float destroyDelay = 5f;
    [SerializeField] float speedController = 1;
    [SerializeField] [Range(0, 1)] float opacityChangedInTime = 1f;
    public float positionY = 0;
    float opacityValue = 1f;


    public void Start()
    {
        pictures = GetComponent<LoopAnimation>()._sprites;
        Destroy(this.gameObject, destroyDelay);
    }

    public void Update()
    {
        {
            Move();
            opacityValue -= opacityChangedInTime * Time.deltaTime;
            OpacityChanger();
        }
    }

    void Move()
    {
        positionY = speedController * Time.deltaTime;
        this.transform.Translate(0, positionY, 0);
    }

    void OpacityChanger()
    {
        foreach (GameObject p in pictures)
        {
            p.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, opacityValue);
        }
    }
}
