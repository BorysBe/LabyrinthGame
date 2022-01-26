using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] Vector3 developerRoomPosition = new Vector3(0f, -100f, 0f);
    public static EnemyFactory Instance;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public Dictionary<string, IPlayStrategy> strategies = new Dictionary<string, IPlayStrategy>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        strategies.Add("Chav", new ChavStrategy());
        strategies.Add("BloodyExplosion", new BloodyExplosionStrategy());
        strategies.Add("ChavGrave", new ChavGraveStrategy());
        strategies.Add("BloodDrop", new BloodDropStrategy());
        foreach (Pool pool in pools)
        {
            var objectPool = new Queue<GameObject>();



            for (int idx = 0; idx < pool.size; idx++)
            {
                var obj = Instantiate(pool.prefab, developerRoomPosition, Quaternion.identity);
                obj.SetActive(true);
                objectPool.Enqueue(obj);

                if (ContainsPlayableChild<EnemyLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<EnemyLifeCycle>(obj, "Chav"); 
                if (ContainsPlayableChild<BloodyExplosionLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<BloodyExplosionLifeCycle>(obj, "BloodyExplosion");
                if (ContainsPlayableChild<GraveLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<GraveLifeCycle>(obj, "ChavGrave");
                if (ContainsPlayableChild<BloodDropLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<BloodDropLifeCycle>(obj, "BloodDrop");
            }

            poolDictionary.Add(pool.key, objectPool);
        }
    }

    public IPlayableGameObject SpawnAtDeveloperRoom(string spawnedEnemy, Transform related)
    {
        return Spawn(spawnedEnemy, GetDeveloperRoomPosition(), related);
    }

    public IPlayableGameObject Spawn(string key, Vector3 position, Transform related)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            throw new Exception("Pool with key " + key + "doesn't exist.");
        }

        GameObject objectToSpawn = GetFromPool(key);
        objectToSpawn.transform.position = position;
        return new PlayableGameObject(objectToSpawn, strategies[key], related);
    }

    public GameObject GetFromPool(string key)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            throw new Exception("Pool with key " + key + "doesn't exist.");
        }
        return poolDictionary[key].Dequeue();
    }

    private void SetFinishingInDeveloperRoom<T>(GameObject obj, string key) where T : IPlayable
    {
        var animation = obj.GetComponent<T>();
        {
            animation.OnFinish += delegate
            {
                obj.transform.position = developerRoomPosition;
                poolDictionary[key].Enqueue(obj);
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
        public string key;
        public GameObject prefab;
        public int size;
    }
}
