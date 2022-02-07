using UnityEngine;

internal class PhantomDeathStrategy : IPlayStrategy
{
    public void Execute(GameObject objectToSpawn, Transform related)
    {
        objectToSpawn.GetComponent<OneTimeAnimation>().Play();
        objectToSpawn.GetComponent<PhantomDeathLifeCycle>().Play();
    }
}