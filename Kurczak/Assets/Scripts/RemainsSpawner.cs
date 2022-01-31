using System.Collections.Generic;
using UnityEngine;

public class RemainsSpawner : MonoBehaviour, IPlayable
{
    public System.Action OnFinish { get; set; }
    public SplashType splashType;
    private ISplashStrategy splashStrategy;
    public GameObject[] RemainsPrefab;
    [SerializeField] int numberOfRemains = 100;
    [SerializeField] float speed = 10f;
    List<GameObject> remainsToSpawn = new List<GameObject>();
    List<GameObject> spawnedRemains = new List<GameObject>();
    [SerializeField] [Range(-10f, 0f)] float xMinRange = -5f;
    [SerializeField] [Range(0f, 10f)] float xMaxRange = 5f;
    [SerializeField] [Range(-5f, -2f)] float yMinRange = -5f;
    [SerializeField] [Range(-2f, 0f)] float yMaxRange = -1f;
    GameObject fragment;
    List<Vector3> finalCoordinates = new List<Vector3>();
    List<Vector3> checkpoints = new List<Vector3>();
    private int timeToRemovefragments = 100;
    float Animation;

    void Start()

    {
        for (int i = 0; i < numberOfRemains; i++)
        {
            remainsToSpawn.Add(RemainsPrefab[UnityEngine.Random.Range(0, RemainsPrefab.Length)]);
            fragment = Instantiate(remainsToSpawn[i], this.transform.position, Quaternion.identity);
            SetStrategy(fragment);
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
            finalCoordinates.Add(new Vector3(UnityEngine.Random.Range(xMinRange, xMaxRange) + transform.position.x, UnityEngine.Random.Range(yMinRange, yMaxRange) + transform.position.y, 0));
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
            splashStrategy?.SplashStop(spawnedRemains[i]);
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
        for (int i = 0; i < numberOfRemains; i++)
        {
            splashStrategy?.Splash(spawnedRemains[i], speed, spawnedRemains[i].transform.position, checkpoints[i], finalCoordinates[i]);            
        }
    }
    private void SetStrategy(GameObject gameObject)
    {
        switch (splashType)
        {
            case SplashType.Parabola:
                splashStrategy = gameObject.AddComponent<ParabolaRoute>();
                break;
            default:
                break;
        }
    }
}
