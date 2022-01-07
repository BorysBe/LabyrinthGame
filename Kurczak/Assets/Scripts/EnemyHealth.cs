using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyHealthbar _healthbar;
    Animator animator;

    public int health = 200;
    int currentHealth;

    public GameObject[] _gunshotWoundPrefab;
    GameObject gunshotWound;

    private void Start()
    {
        currentHealth = health;
        _healthbar.SetHealth(currentHealth, health);
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
    }
}