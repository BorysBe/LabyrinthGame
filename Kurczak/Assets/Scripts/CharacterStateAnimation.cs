using System.Collections.Generic;
using UnityEngine;

public class CharacterStateAnimation : MonoBehaviour
{
    public LoopAnimation move;
    public OneTimeAnimation attack;

    [SerializeField] public List<GameObject> attachedAnimations;
    Vector3 positionOfAnimationStart;
    public void AddAnimation(GameObject animation)
    {
        attachedAnimations.Add(animation);
    }

    public void RemoveAllAnimations()
    {
        attachedAnimations.Clear();
    }

    public void SetPositionOfAnimation(Vector3 vector)
    {
        positionOfAnimationStart = vector;
    }

    public Vector3 ReturnPositionOfAnimation()
    {
        return positionOfAnimationStart;
    }
}
