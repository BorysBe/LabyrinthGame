using System.Collections.Generic;
using UnityEngine;

public class OffsetAndOpacity : MonoBehaviour
{
    List<GameObject> pictures;
    [SerializeField] float moveSpeed = 2;
    Vector3 requestedPositionOffset;
    [SerializeField] float requestedXPositionOffset = 0f;
    [SerializeField] float requestedYPositionOffset = 0f;
    [SerializeField] float requestedZPositionOffset = 0f;
    [SerializeField] [Range(0, 1)] float opacityChangedInTime = 1f;
    float opacityValue = 1f;
    float positionY = 0f;
    float speedController = 2f;

    public string ownerTag => transform.tag;


    public void Start()
    {
        pictures = GetComponent<LoopAnimation>()._sprites;
    }
    void Update()
    {
        Move();
        OpacityChange();
    }
    void Move()
    {
        positionY = speedController * Time.deltaTime;
        this.transform.Translate(0, positionY, 0);
    }
    void OpacityChange()
    {
        opacityValue -= opacityChangedInTime * Time.deltaTime;
        foreach (GameObject p in pictures)
        {
            p.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, opacityValue);
        }
    }
    public void OpacityReset()
    {
        foreach (GameObject p in pictures)
        {
            p.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }  
}
