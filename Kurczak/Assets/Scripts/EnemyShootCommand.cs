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
        timer.Start();
    }

    void TimeOfShootng()
    {
        var chav = GameObject.FindGameObjectWithTag("Chav");
        shot++;
        bool shouldShot = shot % 2 != 0;

        if (shouldShot)
        {
            chav.GetComponent<CharacterStateAnimation>().attack.Play();
            chav.GetComponent<CharacterSounds>().shot.Play();
        }
        else
        {
            chav.GetComponent<CharacterStateAnimation>().attack.Stop();
        }
    }
}