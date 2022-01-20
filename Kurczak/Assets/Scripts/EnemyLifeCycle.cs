using UnityEngine;

public class EnemyLifeCycle : ObjectLifeCycle, IPlayable
{
    [SerializeField] int scoreAdded = 200;
    public EnemyHealthbar _healthbar;
    Animator animator;

    public int health = 200;
    int currentHealth;

    public GameObject[] _gunshotWoundPrefab;
    GameObject gunshotWound;

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

    public void DrawInjuries(Vector2 splatterPosition)
    {
        gunshotWound = Instantiate(_gunshotWoundPrefab[Random.Range(0, _gunshotWoundPrefab.Length)], splatterPosition, Quaternion.identity);
        gunshotWound.transform.SetParent(this.GetComponentInParent<Transform>());
        Vector3 spawnPosition = new Vector3(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y, this.GetComponent<Transform>().position.z);
        Instantiate(_remainsSpawner, spawnPosition, Quaternion.identity);
    }

    public override void Stop()
    {
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