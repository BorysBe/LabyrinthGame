using System.Collections.Generic;
using UnityEngine;

public class LoopAnimation : MonoBehaviour
{
    [SerializeField] int timePerFrameMs = 100;
    public List<GameObject> _sprites = new List<GameObject>();
    private MonoBehaviourUpdateTimer _timer;
    private ChangeSpriteCommand _changeSpriteCommand;

    public void Start()
    {
        _timer = new MonoBehaviourUpdateTimer(timePerFrameMs);
        _changeSpriteCommand = new ChangeSpriteCommand(_sprites, _timer, true);
    }

    public void Update()
    {
        if (_changeSpriteCommand.CanExecute())
        {
            _changeSpriteCommand.Execute();
        }
    }
}

