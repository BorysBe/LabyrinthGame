using System.Collections.Generic;
using UnityEngine;

public class OffsetAndOpacity : MonoBehaviour
{
    List<GameObject> pictures;
    [SerializeField] [Range(0, 1)] float opacityChangedInTime = 1f;
    float opacityValue = 1f;
    float positionY = 0f;
    float speedController = 2f;
    public bool transitionActivated;
    int timeToReset = 4000;

    public void Start()
    {
        pictures = GetComponent<LoopAnimation>()._sprites;
        transitionActivated = false;
    }
    void Update()
    {
        if(transitionActivated)
        {
            MoveUpwards();
            OpacityChange();
        }
    }
    public void Play()
    {
        transitionActivated = true;
        CoroutineTimerReset();
    }
    void MoveUpwards()
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
    void PropertiesReset(CoroutineTimer coroutineTimer)
    {
        foreach (GameObject p in pictures)
        {
            GetComponentInParent<GraveLifeCycle>().Stop();
            opacityValue = 1f;
            transitionActivated = false;
            coroutineTimer.Stop();
        }
    }

    void CoroutineTimerReset()
    {
        CoroutineTimer coroutineTimer = new CoroutineTimer(timeToReset, this);
        coroutineTimer.Tick += delegate ()
        {
            PropertiesReset(coroutineTimer);
        };
        coroutineTimer.Play();
    }


}
