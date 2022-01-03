using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChain
{
    public AnimationChain(params OneTimeAnimation[] animations)
    {
        //animations[0].OnFinish += AddToScene();
    }
}


public class OneTimeAnimation : MonoBehaviour
{
    public List<GameObject> _sprites = new List<GameObject>();
    private MonoBehaviourUpdateTimer _timer;
    private ChangeSpriteCommand _changeSpriteCommand;
    [SerializeField] int timePerFrameMms = 100;

    public void Start()
    {
        _timer = new MonoBehaviourUpdateTimer(timePerFrameMms);
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

