using UnityEngine;

public class MotorcycleLifeCycle : ObjectLifeCycle, IPlayable
{
    [SerializeField] int scoreAdded = 200;
    public EnemyHealthbar _healthbar;
    Animator animator;

    public int health = 200;
    int currentHealth;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        currentHealth = health;
        _healthbar.SetHealth(currentHealth, health);
    }

    public void Damage(int value)
    {
        currentHealth -= value;
        _healthbar.SetHealth(currentHealth, health);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Stop()
    {
        BloodSpringLifeCycle[] bloodDrops = transform.GetComponentsInChildren<BloodSpringLifeCycle>();
        foreach (var b in bloodDrops)
        {
            b.Stop();
        }
        base.Stop();
    }

    public override void Play()
    {
        //currentHealth = health;
    }

    private void AddScore()
    {
        GameObject score = GameObject.FindGameObjectWithTag("Score");
        score.GetComponent<Score>().CountCurrentScore(scoreAdded);
    }

    private void ClearImages()
    {
        SpriteRendererDrawer[] images = transform.GetComponentsInChildren<SpriteRendererDrawer>();
        foreach (var i in images)
        {
            i.ClearImage();
        }
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        AddScore();
        ClearImages();
        currentHealth = health;
        _healthbar.SetHealth(currentHealth, health);
    }
}
