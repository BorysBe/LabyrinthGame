using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeAnimationComposite : MonoBehaviour, IPlayable
{
    [SerializeField] List<OneTimeAnimation> animations = new List<OneTimeAnimation>();

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
