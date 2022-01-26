using UnityEngine;

internal class BloodyExplosionStrategy : IPlayStrategy
{
    public void Execute(GameObject objectToSpawn, Transform related)
    {
        objectToSpawn.GetComponent<OneTimeAnimationComposite>().Play();
        objectToSpawn.GetComponent<BloodyExplosionLifeCycle>().Play();
    }
}