using System;
using System.Collections;
using UnityEngine;

public class CoroutineTimer: IPlayable
{
    private IEnumerator _routine;
    private readonly MonoBehaviour _behaviour;

    public int Interval { get; private set; }
    private readonly float _intervalFloat;

    public Action Tick { get; set; }

    public CoroutineTimer(int interval, MonoBehaviour behaviour)
    {
        Interval = interval;
        _intervalFloat = interval / 1000.0f;
        _behaviour = behaviour;
    }

    public void Play()
    {
        _routine = YieldTick();
        _behaviour.StartCoroutine(_routine);
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
        _behaviour.StopCoroutine(_routine);
    }
}

