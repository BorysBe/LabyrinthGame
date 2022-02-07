using UnityEngine;

internal class MotorcycleExplosionStrategy : IPlayStrategy
{
    public void Execute(GameObject objectToSpawn, Transform related)
    {
        objectToSpawn.GetComponent<OneTimeAnimationComposite>().Play();
        objectToSpawn.GetComponent<MotorcycleExplosionLifeCycle>().Play();
    }
}