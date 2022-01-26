using UnityEngine;

internal class ChavGraveStrategy : IPlayStrategy
{
    public void Execute(GameObject objectToSpawn, Transform related)
    {
        objectToSpawn.GetComponent<LoopAnimation>().Play();
        objectToSpawn.GetComponent<OffsetAndOpacity>().Play();
    }
}