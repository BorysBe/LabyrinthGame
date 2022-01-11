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
        _timer.Update();
    }
}

