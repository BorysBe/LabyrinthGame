
using System;
using UnityEngine;

public interface IPlayableGameObject : IPlayable
{
    GameObject GameObject { get; }
}

public interface IPlayable
{
    void Play();

    void Stop();

    Action OnFinish { get; set; }
}
