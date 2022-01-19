using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] Vector3 developerRoomPosition = new Vector3(0f, -100f, 0f);
    public static EnemyFactory Instance;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            var objectPool = new Queue<GameObject>();

            for (int idx = 0; idx < pool.size; idx++)
            {
                var obj = Instantiate(pool.prefab, developerRoomPosition, Quaternion.identity);
                obj.SetActive(true);
                objectPool.Enqueue(obj);

                if (ContainsPlayableChild<EnemyLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<EnemyLifeCycle>(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnAtDeveloperRoom(string spawnedEnemy, Quaternion identity)
    {
        return Spawn(spawnedEnemy, GetDeveloperRoomPosition(), identity);
    }

    public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            throw new Exception("Pool with tag " + tag + "doesn't exist.");
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        if (objectToSpawn.transform.tag != "Active")
        {
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            poolDictionary[tag].Enqueue(objectToSpawn);
            objectToSpawn.transform.tag = "Active";
            return objectToSpawn;
        }
        else
        {
            poolDictionary[tag].Enqueue(objectToSpawn);
            return null;
        }
    }

    public GameObject attachAnimation(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't excist.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        if (objectToSpawn.transform.tag != "Active")
        {
            poolDictionary[tag].Enqueue(objectToSpawn);
            objectToSpawn.transform.tag = "Active";
            return objectToSpawn;
        }
        else
        {
            poolDictionary[tag].Enqueue(objectToSpawn);
            return null;
        }
    }

    private void SetFinishingInDeveloperRoom<T>(GameObject obj) where T : IPlayable
    {
        var animation = obj.GetComponent<T>();
        {
            animation.OnFinish += delegate
            {
                obj.transform.tag = "Inactive";
                obj.transform.position = developerRoomPosition;
            };
        }
    }

    private static bool ContainsPlayableChild<T>(GameObject obj) where T : IPlayable
    {
        return (obj.GetComponent<T>() != null);
   }

    private Vector3 GetDeveloperRoomPosition()
    {
        return developerRoomPosition;
    }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
}
