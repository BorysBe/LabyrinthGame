using System;
using UnityEngine;

internal class PlayableGameObject : IPlayableGameObject
{
    private GameObject objectToSpawn;
    private readonly IPlayStrategy playStrategy;
    private readonly Transform related;

    public PlayableGameObject(GameObject objectToSpawn, IPlayStrategy playStrategy, Transform related)
    {
        this.objectToSpawn = objectToSpawn;
        this.playStrategy = playStrategy;
        this.related = related;
    }

    public Action OnFinish { get; set; }

    public GameObject GameObject => this.objectToSpawn;

    public void Play()
    {
        playStrategy.Execute(this.objectToSpawn, this.related);
    }

    public void Stop()
    {
        
    }
}