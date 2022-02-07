using UnityEngine;

internal class PortalStrategy : IPlayStrategy
{
    public void Execute(GameObject objectToSpawn, Transform related)
    {
        objectToSpawn.GetComponent<OneTimeAnimation>().Play();
        objectToSpawn.GetComponent<PortalLifeCycle>().Play();
    }
}