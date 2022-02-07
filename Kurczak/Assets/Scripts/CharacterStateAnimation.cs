using UnityEngine;

public class CharacterStateAnimation : MonoBehaviour
{
    public LoopAnimation move;
    public OneTimeAnimation attack;
    Vector3 positionOfAnimationStart;

    public void SetPositionOfAnimation(Vector3 vector)
    {
        positionOfAnimationStart = vector;
    }

    public Vector3 ReturnPositionOfAnimation()
    {
        return positionOfAnimationStart;
    }
}
