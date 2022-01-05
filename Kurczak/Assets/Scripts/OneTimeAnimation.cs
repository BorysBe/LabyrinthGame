using System;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeAnimation : MonoBehaviour
{
    public List<GameObject> _sprites = new List<GameObject>();
    private MonoBehaviourUpdateTimer _timer;
    private ChangeSpriteCommand _changeSpriteCommand;
    [SerializeField] int timePerFrameMs = 100;

    public void Start()
    {
        _timer = new MonoBehaviourUpdateTimer(timePerFrameMs);
        _changeSpriteCommand = new ChangeSpriteCommand(_sprites, _timer, false);
    }

    public void Update()
    {
        _timer.Update();
        if (_changeSpriteCommand.CanExecute())
        {
            _changeSpriteCommand.Execute();
        }
        else
        {
            Destroy(this.gameObject);
            OnFinish?.Invoke();
        }
    }

    public Action OnFinish;
}

