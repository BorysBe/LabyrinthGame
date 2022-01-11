using System.Collections.Generic;
using UnityEngine;

public class LoopAnimation : UpdateTimerMonoBehaviour
{
    public List<GameObject> _sprites = new List<GameObject>();
    private ChangeSpriteCommand _changeSpriteCommand;

    public override void Start()
    {
        base.Start();
        _changeSpriteCommand = new ChangeSpriteCommand(_sprites, _timer, true);
        _changeSpriteCommand.Execute();
    }

    public override void Update()
    {
        base.Update();
        if (_changeSpriteCommand.CanExecute())
        {
            _changeSpriteCommand.Execute();
        }
    }
}

