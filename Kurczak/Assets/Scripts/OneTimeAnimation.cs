using System;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeAnimation : UpdateTimerMonoBehaviour, IPlayable
{
    public List<GameObject> _sprites = new List<GameObject>();
    private ChangeSpriteCommand _changeSpriteCommand;
    [SerializeField] bool destroyWhenEnded = false;

    public override void Start()
    {
        base.Start();
        _timer = new MonoBehaviourUpdateTimer(timePerFrameMs);
        _changeSpriteCommand = new ChangeSpriteCommand(_sprites, _timer, false, firstFrameActive);
    }

    public override void Update()
    {
        base.Update();
        if (_changeSpriteCommand.CanExecute())
        {
            _changeSpriteCommand.Execute();
        }
        else
        {
            if (destroyWhenEnded)
            {
                Destroy(this.gameObject);
            }

            OnFinish?.Invoke();
        }
    }

    public override void Stop()
    {
        _changeSpriteCommand.ForceReset();
    }

    public override void Play()
    {
        _changeSpriteCommand.Execute();
    }

    public Action OnFinish;
}

