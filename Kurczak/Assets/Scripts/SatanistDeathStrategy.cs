using UnityEngine;

internal class SatanistDeathStrategy : IPlayStrategy
{
    public void Execute(GameObject objectToSpawn, Transform related)
    {
        objectToSpawn.GetComponent<LoopAnimation>().Play();
        objectToSpawn.GetComponent<JumpAndRotation>().Play();
    }
}