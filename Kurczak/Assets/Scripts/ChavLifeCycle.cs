using UnityEngine;

public class ChavLifeCycle : ObjectLifeCycle, IPlayable
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
        var canvas = transform.Find("HitboxCanvas");
        CanvasRenderer[] hitboxes = canvas.GetComponentsInChildren<CanvasRenderer>();
        foreach (var h in hitboxes)
        {
            var woundArea = EnemyFactory.Instance.Spawn(PrefabType.ChavWoundArea, transform.position, null);
            woundArea.GameObject.transform.parent = h.transform;
        }
    }

    private void AddScore()
    {
        GameObject score = GameObject.FindGameObjectWithTag("Score");
        score.GetComponent<Score>().CountCurrentScore(scoreAdded);
    }

    private void DeleteWoundAreas()
    {
        ChavWoundAreaLifeCycle[] images = transform.GetComponentsInChildren<ChavWoundAreaLifeCycle>();
        foreach (var i in images)
        {
            i.Stop();
        }
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        AddScore();
        DeleteWoundAreas();
        currentHealth = health;
        _healthbar.SetHealth(currentHealth, health);
    }
}