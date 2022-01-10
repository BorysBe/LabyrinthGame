using System;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeAnimation : MonoBehaviour
{
    public List<GameObject> _sprites = new List<GameObject>();
    private MonoBehaviourUpdateTimer _timer;
    private ChangeSpriteCommand _changeSpriteCommand;
    [SerializeField] int timePerFrameMs = 100;
    [SerializeField] bool destroyWhenEnded = false;

    void Start()
    {
        _timer = new MonoBehaviourUpdateTimer(timePerFrameMs);
        _changeSpriteCommand = new ChangeSpriteCommand(_sprites, _timer, false);
        _changeSpriteCommand.Execute();
    }

    void Update()
    {
        _timer.Update();
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

    public void Stop()
    {
        _changeSpriteCommand.ForceReset();
    }

    public void Play()
    {
        _changeSpriteCommand.Execute();
    }

    public Action OnFinish;
}

