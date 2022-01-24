using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeCycle : ObjectLifeCycle, IPlayable
{
    [SerializeField] int scoreAdded = 200;
    public EnemyHealthbar _healthbar;
    Animator animator;

    public int health = 200;
    int currentHealth;

    public RemainsSpawner _remainsSpawner;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void Damage(int value)
    {
        currentHealth -= value;
        _healthbar.SetHealth(currentHealth, health);
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }

    public override void Stop()
    {
        SpriteRenderer[] renderers = transform.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var r in renderers)
        {
            r.transform.GetComponent<SpriteMaker>().ResetTexture();
        }
        GameObject score = GameObject.FindGameObjectWithTag("Score");
        score.GetComponent<Score>().CountCurrentScore(scoreAdded);
        base.Stop();
    }

    public override void Play()
    {
        currentHealth = health;
        _healthbar.SetHealth(currentHealth, health);
    }
}