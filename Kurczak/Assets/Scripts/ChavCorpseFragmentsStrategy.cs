using UnityEngine;

internal class ChavCorpseFragmentsStrategy : IPlayStrategy
{
    public void Execute(GameObject objectToSpawn, Transform related)
    {
        objectToSpawn.GetComponent<RemainsSpawner>().Play();
        objectToSpawn.GetComponent<ChavCorpseFragmentsLifeCycle>().Play();
    }
}
