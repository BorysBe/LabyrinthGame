using UnityEngine;

internal class BloodDropStrategy : IPlayStrategy
{
    public void Execute(GameObject objectToSpawn, Transform related)
    {
        objectToSpawn.GetComponent<OneTimeAnimation>().Play();
        objectToSpawn.GetComponent<BloodDropLifeCycle>().Play();
        objectToSpawn.transform.SetParent(related.transform);
    }
}