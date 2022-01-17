using System;
using System.Collections.Generic;
using UnityEngine;

public class LoopAnimation : UpdateTimerMonoBehaviour, IPlayable
{
    protected override bool isLooped { get; } = true;
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

    public override void Play()
    {
        _changeSpriteCommand.Execute();
    }

    public override void Stop()
    {
        //Intentionally left blank.
    }
}

