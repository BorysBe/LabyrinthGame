using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    [SerializeField] Vector3 developerRoomPosition = new Vector3(0f, -100f, 0f);

    public static EnemyFactory Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, Vector3> _spawnPoints = new Dictionary<string, Vector3>();

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, developerRoomPosition, Quaternion.identity);
                obj.SetActive(true);
                objectPool.Enqueue(obj);
                
                var animation = obj.GetComponent<OneTimeAnimationComposite>();
                if (animation)
                {
                    animation.OnFinish += delegate
                    {
                        obj.transform.position = developerRoomPosition;
                    };
                }
                var animation2 = obj.GetComponent<OneTimeAnimation>();
                if (animation2)
                {
                    animation2.OnFinish += delegate
                    {
                        obj.transform.position = developerRoomPosition;
                    };
                }
             }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject Spawn(string tag, Quaternion rotation)
    {
        return Spawn(tag, _spawnPoints[tag], rotation);
    }

    public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't excist.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public void SetSpawnPointFor(string tag, Vector3 position)
    {
        _spawnPoints[tag] = position;
    }
}
