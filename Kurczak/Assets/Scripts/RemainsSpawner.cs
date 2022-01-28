using System;
using System.Collections.Generic;
using UnityEngine;

public class RemainsSpawner : MonoBehaviour, IPlayable
{
    public System.Action OnFinish { get; set; }
    public GameObject[] RemainsPrefab;
    [SerializeField] int numberOfRemains = 100;
    [SerializeField] float moveSpeed = 2f;
    List<GameObject> remainsToSpawn = new List<GameObject>();
    List<GameObject> spawnedRemains = new List<GameObject>();
    [SerializeField][Range(-10f, 0f)] float xMinRange = -5f;
    [SerializeField] [Range(0f, 10f)] float xMaxRange = 5f;
    [SerializeField] [Range(-5f, -2f)] float yMinRange = -5f;
    [SerializeField] [Range(-2f, 0f)] float yMaxRange = -1f;
    GameObject fragment;
    List<Vector3> finalCoordinates = new List<Vector3>();
    List<Vector3> checkpoints = new List<Vector3>();
    private int timeToRemovefragments = 1000;
    float Animation;

    void Start()

    {
        for (int i = 0; i < numberOfRemains; i ++)
        {
            remainsToSpawn.Add(RemainsPrefab[UnityEngine.Random.Range(0, RemainsPrefab.Length)]);
            fragment = Instantiate(remainsToSpawn[i], this.transform.position, Quaternion.identity);
            fragment.AddComponent<ParabolaRoute>();
            fragment.transform.SetParent(this.GetComponentInParent<Transform>());
            spawnedRemains.Add(fragment);
        }
        Animation += Time.deltaTime;
        Animation = Animation % 5f;
    }

    public void Play()
    {
        SetFinalCoordinates();
        SetCheckpoints();
        Move();
    }

    private void SetFinalCoordinates()
    {
        for (int i = 0; i < numberOfRemains; i++)
        {
            finalCoordinates.Add(new Vector3(UnityEngine.Random.Range(xMinRange, xMaxRange) + transform.position.x, UnityEngine.Random.Range(yMinRange, yMaxRange), 0)) ;
        }
    }

    private void SetCheckpoints()
    {
        for (int i = 0; i < numberOfRemains; i++)
        {
            checkpoints.Add(new Vector3(UnityEngine.Random.Range(transform.position.x, finalCoordinates[i].x), UnityEngine.Random.Range(finalCoordinates[i].y, transform.position.y), 0));
        }
    }

    public void Stop()
    {
        for (int i = 0; i < numberOfRemains; i++)
        {
            spawnedRemains[i].transform.position = transform.position;
            spawnedRemains[i].GetComponent<ParabolaRoute>().Stop();
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
            var rout = spawnedRemains[i].GetComponent<ParabolaRoute>();
            rout.AddChceckpoints(spawnedRemains[i].transform.position, checkpoints[i], finalCoordinates[i]);
            rout.Play();
        }
    }

}
