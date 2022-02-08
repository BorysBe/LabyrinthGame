using UnityEngine;

public class RotatePerSecond: MonoBehaviour
{
    public void RotateMultiple(Vector3 rotationDirection, float rotationsPerSecond, GameObject[] gameObjects)
    {
        float smooth = Time.deltaTime * rotationsPerSecond * 200;
        foreach (var o in gameObjects)
        {
            o.transform.Rotate(rotationDirection * smooth);
        }
    }
    public void Rotate(Vector3 rotationDirection, float rotationsPerSecond, GameObject gameObject)
    {
        float smooth = Time.deltaTime * rotationsPerSecond * 200;
        gameObject.transform.Rotate(rotationDirection * smooth);
    }
}
