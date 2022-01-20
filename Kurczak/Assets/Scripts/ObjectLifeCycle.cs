using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifeCycle :MonoBehaviour, IPlayable
{
    public Action OnFinish { get; set; }

    public virtual void Play()
    {
    }

    public virtual void Stop()
    {
        OnFinish?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
