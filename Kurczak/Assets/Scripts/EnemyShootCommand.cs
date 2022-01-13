using System;
using UnityEngine;

public class EnemyShootCommand: MonoBehaviour
{
    private CoroutineTimer timer;
    int shot = 0;
    [SerializeField] int timeOfAnimation = 500;
    public void Execute()
    {
        Debug.Log("Attack!");
        if (shot > 0)
            return;
        timer = new CoroutineTimer(timeOfAnimation, this);
        timer.Tick = TimeOfShootng;
        timer.Play();
    }

    public void Stop()
    {
        shot = 0;
        timer.Stop();
    }

    void TimeOfShootng()
    {
        shot++;
        bool shouldShot = shot % 2 != 0;

        if (shouldShot)
        {
            GetComponentInParent<CharacterStateAnimation>().attack.Play();
            GetComponentInParent<CharacterSounds>().shot.Play();
        }
        else
        {
            GetComponentInParent<CharacterStateAnimation>().attack.Stop();
            GetComponentInParent<CharacterSounds>().shot.Stop();
        }
    }
}