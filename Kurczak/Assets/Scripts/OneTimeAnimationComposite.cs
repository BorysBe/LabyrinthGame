using System;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeAnimationComposite : MonoBehaviour, IPlayable
{
    [SerializeField] List<OneTimeAnimation> animations = new List<OneTimeAnimation>();
    [SerializeField] bool actionOnFinish = true;

    public void Start()
    {
        foreach (var a in animations)
        {
            a.OnFinish += delegate ()
            {
                if (actionOnFinish)
                    OnFinish();
            };
        }
    }

    public void Play()
    {
        foreach (var a in animations)
        {
            a.Play();
        }
    }

    public void Stop()
    {
        foreach (var a in animations)
        {
            a.Stop();
        }
    }

    public Action OnFinish;
}
