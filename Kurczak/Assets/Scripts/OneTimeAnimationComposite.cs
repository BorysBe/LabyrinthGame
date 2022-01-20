using System;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeAnimationComposite : MonoBehaviour, IPlayable
{
    [SerializeField] List<OneTimeAnimation> animations = new List<OneTimeAnimation>();

    public Action OnFinish { get; set; }

    public void Start()
    {
        foreach (var a in animations)
        {
            a.OnFinish += delegate ()
            {
                OnFinish?.Invoke();
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
}
