using System;
using System.Collections.Generic;
using UnityEngine;

public class LoopAnimation : UpdateTimerMonoBehaviour, IPlayable
{

    public List<GameObject> _sprites = new List<GameObject>();
    private ChangeSpriteCommand _changeSpriteCommand;

    public override void Start()
    {
        base.Start();
        _changeSpriteCommand = new ChangeSpriteCommand(_sprites, _timer, true, firstFrameActive);
    }

    public override void Update()
    {
        base.Update();
        if (_changeSpriteCommand?.CanExecute()??false)
        {
            _changeSpriteCommand.Execute();
        }
    }

    public override void Play()
    {
        _changeSpriteCommand?.Execute();
    }

    public override void Stop()
    {
        //Intentionally left blank.
    }
}

