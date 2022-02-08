using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] Vector3 developerRoomPosition = new Vector3(0f, -100f, 0f);
    public static EnemyFactory Instance;
    public List<Pool> pools;
    public Dictionary<PrefabType, Queue<GameObject>> poolDictionary;
    public Dictionary<PrefabType, IPlayStrategy> strategies = new Dictionary<PrefabType, IPlayStrategy>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<PrefabType, Queue<GameObject>>();
        strategies.Add(PrefabType.Chav, new ChavStrategy());
        strategies.Add(PrefabType.BloodyExplosion, new BloodyExplosionStrategy());
        strategies.Add(PrefabType.ChavGrave, new ChavGraveStrategy());
        strategies.Add(PrefabType.BloodSpring, new BloodSpringStrategy());
        strategies.Add(PrefabType.ChavCorpseFragments, new ChavCorpseFragmentsStrategy());
        strategies.Add(PrefabType.Satanist, new SatanistStrategy());
        strategies.Add(PrefabType.Motorcycle, new MotorcycleStrategy());
        strategies.Add(PrefabType.ChavWoundArea, new ChavWoundAreaStrategy());
        strategies.Add(PrefabType.SatanistWoundArea, new SatanistWoundAreaStrategy());
        strategies.Add(PrefabType.MotorcycleWoundArea, new MotorcycleWoundAreaStrategy());
        strategies.Add(PrefabType.MotorcycleExplosion, new MotorcycleExplosionStrategy());
        strategies.Add(PrefabType.Phantom, new PhantomStrategy());
        strategies.Add(PrefabType.PhantomWoundArea, new PhantomWoundAreaStrategy());
        strategies.Add(PrefabType.PhantomDeath, new PhantomDeathStrategy());
        strategies.Add(PrefabType.Portal, new PortalStrategy());
        strategies.Add(PrefabType.RotatingSatanist, new RotatingSatanistStrategy());
        strategies.Add(PrefabType.SatanistDeath, new SatanistDeathStrategy());
        foreach (Pool pool in pools)
        {
            var objectPool = new Queue<GameObject>();

            for (int idx = 0; idx < pool.size; idx++)
            {
                var obj = Instantiate(pool.prefab, developerRoomPosition, Quaternion.identity);
                obj.SetActive(true);
                objectPool.Enqueue(obj);

                if (ContainsPlayableChild<ChavLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<ChavLifeCycle>(obj, PrefabType.Chav); 
                if (ContainsPlayableChild<BloodyExplosionLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<BloodyExplosionLifeCycle>(obj, PrefabType.BloodyExplosion);
                if (ContainsPlayableChild<GraveLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<GraveLifeCycle>(obj, PrefabType.ChavGrave);
                if (ContainsPlayableChild<BloodSpringLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<BloodSpringLifeCycle>(obj, PrefabType.BloodSpring);
                if (ContainsPlayableChild<ChavCorpseFragmentsLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<ChavCorpseFragmentsLifeCycle>(obj, PrefabType.ChavCorpseFragments);
                if (ContainsPlayableChild<SatanistLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<SatanistLifeCycle>(obj, PrefabType.Satanist);
                if (ContainsPlayableChild<MotorcycleLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<MotorcycleLifeCycle>(obj, PrefabType.Motorcycle);
                if (ContainsPlayableChild<MotorcycleExplosionLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<MotorcycleExplosionLifeCycle>(obj, PrefabType.Motorcycle);
                if (ContainsPlayableChild<PhantomLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<PhantomLifeCycle>(obj, PrefabType.Phantom);
                if (ContainsPlayableChild<PhantomDeathLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<PhantomDeathLifeCycle>(obj, PrefabType.PhantomDeath);
                if (ContainsPlayableChild<PortalLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<PortalLifeCycle>(obj, PrefabType.Portal);
                if (ContainsPlayableChild<RotatingSatanistLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<RotatingSatanistLifeCycle>(obj, PrefabType.RotatingSatanist);
                if (ContainsPlayableChild<SatanistDeathLifeCycle>(obj))
                    SetFinishingInDeveloperRoom<SatanistDeathLifeCycle>(obj, PrefabType.SatanistDeath);
            }

            poolDictionary.Add(pool.key, objectPool);
        }
    }

    public IPlayableGameObject SpawnAtDeveloperRoom(PrefabType spawnedEnemy, Transform related)
    {
        return Spawn(spawnedEnemy, GetDeveloperRoomPosition(), related);
    }

    public IPlayableGameObject Spawn(PrefabType key, Vector3 position, Transform related)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            throw new Exception("Pool with key " + key + "doesn't exist.");
        }

        GameObject objectToSpawn = GetFromPool(key);
        objectToSpawn.transform.position = position;
        return new PlayableGameObject(objectToSpawn, strategies[key], related);
    }

    public GameObject GetFromPool(PrefabType key)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            throw new Exception("Pool with key " + key + "doesn't exist.");
        }
        return poolDictionary[key].Dequeue();
    }

    private void SetFinishingInDeveloperRoom<T>(GameObject obj, PrefabType key) where T : IPlayable
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
        public PrefabType key;
        public GameObject prefab;
        public int size;
    }
}
