using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack: MonoBehaviour
{
    public Chavs _chavs;
    private CoroutineTimer timer;
    int shot = 0;
    public void Shot()
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
        var spriteRenderer = this.GetComponentInParent<SpriteRenderer>();

        shot++;
        if (shot % 2 == 0)
        {
            spriteRenderer.sprite = _chavs._sprites[0];
        }
        else
        {
            spriteRenderer.sprite = _chavs._sprites[1];
        }
    }
}