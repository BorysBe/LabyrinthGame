using System.Collections.Generic;
using UnityEngine;

public class JumpAndRotation : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float opacityChangedInTime = 1f;
    float opacityValue = 1f;
    float positionY = 0f;
    float speedController = 2f;
    public bool transitionActivated;
    int timeToReset = 4000;

    public void Start()
    {
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
        transitionActivated = true;
        CoroutineTimerReset();
    }
    void MoveUpwards()
    {
        positionY = speedController * Time.deltaTime;
        this.transform.Translate(0, positionY, 0);
    }

    void MoveDown(CoroutineTimer coroutineTimer)
    {
        positionY = speedController * Time.deltaTime;
        this.transform.Translate(0, - positionY, 0);
    }

    void CoroutineTimerReset()
    {
        CoroutineTimer coroutineTimer = new CoroutineTimer(timeToReset, this);
        coroutineTimer.Tick += delegate ()
        {
            MoveDown(coroutineTimer);
        };
        coroutineTimer.Play();
    }


}
