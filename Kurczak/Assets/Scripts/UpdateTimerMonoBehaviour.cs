using System.Diagnostics;
using UnityEngine;

public class UpdateTimerMonoBehaviour : MonoBehaviour
{
    protected MonoBehaviourUpdateTimer _timer;
    [SerializeField] protected int timePerFrameMs = 100;
    
    public virtual void Start()
    {
        _timer = new MonoBehaviourUpdateTimer(timePerFrameMs);
    }

    public virtual void Update()
    {
        if (_timer == null)
            Debugger.Break();
        _timer.Update();
    }
}

