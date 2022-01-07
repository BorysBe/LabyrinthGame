using System;
using UnityEngine;

public class EnemyShootCommand: MonoBehaviour
{
    private CoroutineTimer timer;
    int shot = 0;

    public void Execute()
    {
        Debug.Log("Attack!");
        if (shot > 0)
            return;

        timer = new CoroutineTimer(500, this);
        timer.Tick = TimeOfShootng;
        timer.Start();
    }

    void TimeOfShootng()
    {
        shot++;
        bool shouldShot = shot % 2 != 0;

        if (shouldShot)
        {
            var chav = GameObject.FindGameObjectWithTag("Chav");
            chav.GetComponent<CharacterStateAnimation>().attack.enabled = true;
        }
        else
            RestartOneTimeAnimation();
    }

    private void RestartOneTimeAnimation()
    {
        var chav = GameObject.FindGameObjectWithTag("Chav");
        chav.GetComponent<CharacterStateAnimation>().attack.Reset();
    }
}