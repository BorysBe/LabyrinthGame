using System.Diagnostics;
using UnityEngine;

public class UpdateTimerMonoBehaviour : MonoBehaviour, IPlayable
{
    [SerializeField] protected bool playOnAwake = false;
    [SerializeField] protected bool firstFrameActive = true;
    protected MonoBehaviourUpdateTimer _timer;
    [SerializeField] protected int timePerFrameMs = 100;
    public string ownerTag => transform.tag;
    
    public virtual void Start()
    {
        _timer = new MonoBehaviourUpdateTimer(timePerFrameMs);
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

    }

    public virtual void Update()
    {
        if (_timer == null)
            Debugger.Break();
        _timer.Update();
    }
}

