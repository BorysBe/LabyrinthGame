using System;
using UnityEngine;

public class OneTimeAnimation : UpdateTimerMonoBehaviour, IPlayable
{
    protected override bool isLooped { get; } = false;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (_changeSpriteCommand.CanExecute())
        {
            _changeSpriteCommand.Execute();
        }
    }

    public override void Stop()
    {
        base.Stop();
        _changeSpriteCommand.ForceReset();
    }

    public override void Play()
    {
        _changeSpriteCommand.Execute();
    }
}

