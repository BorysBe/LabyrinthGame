using System;
using UnityEngine;

public class MonoBehaviourUpdateTimer
{
    private float time;
    private const int spriteChangeDelay = 1;

    public int Interval { get; private set; }

    public Action Tick { get; set; }
    public Action _internalUpdate;

    public MonoBehaviourUpdateTimer(int interval)
    {
        Interval = interval;
        _internalUpdate = InternalUpdateNullAction;
    }

    public void Start()
    {
        _internalUpdate = InternalUpdateAction;
    }

    public void Stop()
    {
        _internalUpdate = InternalUpdateNullAction;
    }

    float SetTime()
    {
        float timePerFrame;
        timePerFrame = Time.deltaTime * 1000 / Interval;
        return timePerFrame;
    }

    /// <summary>
    /// Call it every time when MonoBehaviour instance call it's Update
    /// </summary>
    public void Update()
    {
        _internalUpdate.Invoke();
    }

    private void InternalUpdateAction()
    {
        time += SetTime();
        if (time >= spriteChangeDelay)
        {
            time = 0;
            Tick?.Invoke();
        }
    }

    private void InternalUpdateNullAction()
    {
        // intentrionally left blank
    }
}

