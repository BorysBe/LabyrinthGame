using UnityEngine;

public interface IPlayStrategy
{
    void Execute(GameObject objectToSpawn, Transform related);
}