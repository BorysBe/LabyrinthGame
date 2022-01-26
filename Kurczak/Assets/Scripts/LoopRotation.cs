using System;
using UnityEngine;

public class LoopRotation : MonoBehaviour, IPlayable
{
    public Action OnFinish { get; set; }
    Transform parentTransform;
    [SerializeField] int timeToFullRotationMs = 1000;
    [SerializeField] bool isLooped = true;

    void Start()
    {
        parentTransform = transform.GetComponent<Transform>();
    }

    void Update()
    {
        
    }
    public void Play()
    {

    }

    public void Stop()
    {

    }
}
