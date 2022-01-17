using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpdateTimerMonoBehaviour : MonoBehaviour, IPlayable
{
    public List<GameObject> _sprites = new List<GameObject>();
    protected ChangeSpriteCommand _changeSpriteCommand;
    [SerializeField] protected bool playOnAwake = false;
    protected MonoBehaviourUpdateTimer _timer;
    [SerializeField] protected int timePerFrameMs = 100;
    [SerializeField] protected bool actionOnFinish = false;
    public Action OnFinish;

    protected abstract bool isLooped { get; }

    public string ownerTag => transform.tag;
    
    public virtual void Start()
    {
        _timer = new MonoBehaviourUpdateTimer(timePerFrameMs);
        _changeSpriteCommand = new ChangeSpriteCommand(_sprites, _timer, isLooped);
        _changeSpriteCommand.OnFinish += delegate
        {
            this.Stop();
        };
        if (playOnAwake)
        {
            this.Play();
        }
    }

    public virtual void Play()
    {
        
    }

    public virtual void Stop()
    {
        if (actionOnFinish)
        {
            OnFinish?.Invoke();
        }
    }

    public virtual void Update()
    {
        _timer.Update();
    }
}

