using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanistDieAnimation : MonoBehaviour
{
    List<GameObject> pictures;
    [SerializeField] [Range(0, 1)] float opacityChangedInTime = 1f;
    float opacityValue = 1f;
    float positionY = 0f;
    float speedController = 2f;
    public bool transitionActivated;
    int timeToReset = 4000;
    Vector3 InitiativePosition;

    public void Start()
    {
        pictures = GetComponent<LoopAnimation>()._sprites;
        transitionActivated = false;
    }
    void Update()
    {
        if (transitionActivated)
        {
            MoveUpwards();
        }
    }
    public void Play()
    {
        InitiativePosition = this.transform.position;
        transitionActivated = true;
        CoroutineTimerReset();
    }
void MoveUpwards()
    {
        positionY = speedController * Time.deltaTime;
        this.transform.Translate(0, positionY, 0);
    }
    void PropertiesReset(CoroutineTimer coroutineTimer)
    {
        foreach (GameObject p in pictures)
        {
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
