using System;
using UnityEngine;

public class RotatingSatanistLifeCycle : ObjectLifeCycle, IPlayable
{
    [SerializeField] int scoreAdded = 200;
    [SerializeField] Vector3 rotationDirection = new Vector3(0f, 0f, 20f);
    [SerializeField] float rotationsPerSecond = 1f;
    [SerializeField] GameObject[] rotatingObjects;
    Animator animator;
    RotatePerSecond rotate;
    public EnemyHealthbar _healthbar;
    public int health = 200;
    private int currentHealth;

    private Action RotateAction { get; set; }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        rotate = this.GetComponent<RotatePerSecond>();
        currentHealth = health;
        _healthbar.SetHealth(currentHealth, health);
        RotateAction = NullObjectMove;
    }

    private void NullObjectMove()
    {
        // intentionally left blank
    }

    void Update()
    {
        RotateAction.Invoke();
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
        RotateAction = NullObjectMove;
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
            var woundArea = EnemyFactory.Instance.Spawn(PrefabType.SatanistWoundArea, transform.position, null);
            woundArea.GameObject.transform.parent = h.transform;
        }

        RotateAction = Rotate;
    }

    private void AddScore()
    {
        GameObject score = GameObject.FindGameObjectWithTag("Score");
        score.GetComponent<Score>().CountCurrentScore(scoreAdded);
    }

    private void DeleteWoundAreas()
    {
        SatanistWoundAreaLifeCycle[] images = transform.GetComponentsInChildren<SatanistWoundAreaLifeCycle>();
        foreach (var i in images)
        {
            i.Stop();
        }
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        RotateAction = NullObjectMove;
        AddScore();
        DeleteWoundAreas();
        currentHealth = health;
        _healthbar.SetHealth(currentHealth, health);
    }
    void Rotate()
    {
        rotate.RotateMultiple(rotationDirection, rotationsPerSecond, rotatingObjects);
    }
}
