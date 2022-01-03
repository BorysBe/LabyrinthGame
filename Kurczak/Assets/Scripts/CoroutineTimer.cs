using System;
using System.Collections;
using UnityEngine;

public class CoroutineTimer
{
    private float time;
    private const int spriteChangeDelay = 1;
    private readonly MonoBehaviour _behaviour;

    public int Interval { get; private set; }

    private readonly float _intervalFloat;

    public Action Tick { get; set; }
    public Action _internalUpdate;

    public CoroutineTimer(int interval, MonoBehaviour behaviour)
    {
        Interval = interval;
        _intervalFloat = interval / 1000.0f;
        _behaviour = behaviour;
        _internalUpdate = InternalUpdateNullAction;
    }

    public void Start()
    {
        _internalUpdate = InternalUpdateAction;
        _behaviour.StartCoroutine(YieldTick());
    }

    private IEnumerator YieldTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(_intervalFloat);
            Tick?.Invoke();
        }
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
    private void Update()
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

