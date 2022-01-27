using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainsSpawner : MonoBehaviour, IPlayable
{
    public System.Action OnFinish { get; set; }
    private Action MoveAction { get; set; }
    public GameObject[] RemainsPrefab;
    [SerializeField] int numberOfRemains = 100;
    [SerializeField] float moveSpeed = 2f;
    List<GameObject> remainsToSpawn = new List<GameObject>();
    List<GameObject> spawnedRemains = new List<GameObject>();
    [SerializeField][Range(-5f, 0f)] float xMinRange = -5f;
    [SerializeField] [Range(0f, -5f)] float xMaxRange = 5f;
    [SerializeField] [Range(-5f, -2f)] float yMinRange = -5f;
    [SerializeField] [Range(-2f, 0f)] float yMaxRange = -1f;
    GameObject fragment;
    List<Vector3> finalCoordinates = new List<Vector3>();
    private int timeToRemovefragments = 1000;

    void Start()

    {
        for (int i = 0; i < numberOfRemains; i ++)
        {
            remainsToSpawn.Add(RemainsPrefab[UnityEngine.Random.Range(0, RemainsPrefab.Length)]);
            fragment = Instantiate(remainsToSpawn[i], this.transform.position, Quaternion.identity);
            fragment.transform.SetParent(this.GetComponentInParent<Transform>());
            spawnedRemains.Add(fragment);
        }
        MoveAction = NullObjectMove;
    }

    void Update()
    {
        MoveAction.Invoke();
    }

    public void Play()
    {
        SetFinalCoordinates();
        MoveAction = Move;
    }

    private void SetFinalCoordinates()
    {
        for (int i = 0; i < numberOfRemains; i++)
        {
            finalCoordinates.Add(new Vector3(UnityEngine.Random.Range(xMinRange, xMaxRange) + transform.position.x, UnityEngine.Random.Range(yMinRange, yMaxRange), 0)) ;
        }

    }

    public void Stop()
    {
        MoveAction = NullObjectMove;
        for (int i = 0; i < numberOfRemains; i++)
        {
            spawnedRemains[i].transform.position = transform.position;
        }
        OnFinish?.Invoke();
    }

    void Move()
    {
        CoroutineTimer coroutineTimer = new CoroutineTimer(timeToRemovefragments, this);
        coroutineTimer.Tick += delegate ()
        {
            coroutineTimer.Stop();
            Stop();
        };
        coroutineTimer.Play();
        var movementThisFrame = moveSpeed * Time.deltaTime;
        for (int i = 0; i < numberOfRemains; i++)
        {
            spawnedRemains[i].transform.position = Vector2.MoveTowards(spawnedRemains[i].transform.position, finalCoordinates[i], movementThisFrame);
        }
    }

    private void NullObjectMove()
    {
        // intentionally left blank
    }
}
