using System;
using System.Collections.Generic;
using UnityEngine;

public class RemainsSpawner : MonoBehaviour, IPlayable
{
    public System.Action OnFinish { get; set; }
    public SplashType splashType;
    private ISplashStrategy splashStrategy;
    public Texture2D[] textures;
    //public GameObject[] RemainsPrefab;
    [SerializeField] int numberOfRemains = 100;
    [SerializeField] float speed = 10f;
    List<Texture2D> listOfUsedTextures = new List<Texture2D>();
    List<GameObject> remainsToSpawn = new List<GameObject>();
    List<GameObject> spawnedRemains = new List<GameObject>();
    [SerializeField] [Range(-10f, 0f)] float xMinRange = -5f;
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
        for (int i = 0; i < numberOfRemains; i++)
        {
            fragment = Instantiate(new GameObject("Object"), this.transform.position, Quaternion.identity);
            remainsToSpawn.Add(fragment);
            remainsToSpawn[i].AddComponent<SpriteRenderer>();
            listOfUsedTextures.Add(textures[UnityEngine.Random.Range(0, textures.Length)]);
            remainsToSpawn[i].GetComponent<SpriteRenderer>().sprite = Sprite.Create(listOfUsedTextures[i], new Rect(0.0f, 0.0f, listOfUsedTextures[i].width, listOfUsedTextures[i].height), new Vector2(0.5f, 0.5f), 100.0f);
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
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        for (int i = 0; i < numberOfRemains; i++)
        {
            finalCoordinates.Add(new Vector3(UnityEngine.Random.Range(xMinRange, xMaxRange) + transform.position.x, UnityEngine.Random.Range(yMinRange, yMaxRange), 0));
        }
    }

    private void SetCheckpoints()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        for (int i = 0; i < numberOfRemains; i++)
        {
            checkpoints.Add(new Vector3(UnityEngine.Random.Range(transform.position.x, finalCoordinates[i].x), UnityEngine.Random.Range(finalCoordinates[i].y, transform.position.y), 0));
        }
    }

    public void Stop()
    {
        var cam = Camera.main;
        var terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponentInChildren<SpriteRendererDrawer>();
        for (int i = 0; i < numberOfRemains; i++)
        {
            var position = cam.WorldToScreenPoint(new Vector3(spawnedRemains[i].transform.position.x, - spawnedRemains[i].transform.position.y, spawnedRemains[i].transform.position.z));
            terrain.DrawSpriteWithDefiniedSprite(position, listOfUsedTextures[i]);
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
