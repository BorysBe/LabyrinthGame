using UnityEngine;

internal class BloodSpringStrategy : IPlayStrategy
{
    public void Execute(GameObject objectToSpawn, Transform related)
    {
        objectToSpawn.GetComponent<OneTimeAnimation>().Play();
        objectToSpawn.GetComponent<BloodSpringLifeCycle>().Play();
        objectToSpawn.transform.SetParent(related.transform);
    }
}