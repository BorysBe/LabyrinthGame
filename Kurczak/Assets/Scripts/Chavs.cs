using System;
using UnityEngine;
using System.Collections;

public class Chavs : MonoBehaviour
{
    public EnemyHealthbar _healthbar;
    public AtlasLoader _loader;

    public int health = 200;
    int currentHealth;

    public GameObject[] _gunshotWoundPrefab;
    GameObject gunshotWound;

    public Action ActiveDrawing { get; private set; }
    public Action ActiveSound { get; private set; }
    private CoroutineTimer timer;
    int shot = 0;

    Animator animator;
    GameObject chav;

    void Start()
    {
        chav = GameObject.FindGameObjectWithTag("Chav");
        animator = this.GetComponent<Animator>();
        _loader = new AtlasLoader(@"Sprites\Enemies");
        var hitboxCanvas = GameObject.FindGameObjectWithTag("Hitbox Canvas");
        var chavToucherBody = GameObject.FindGameObjectWithTag("ChavToucherBody");
        var chavToucherHead = GameObject.FindGameObjectWithTag("ChavToucherHead");
        currentHealth = health;
        _healthbar.SetHealth(currentHealth, health);
    }

    public Vector3 WorldPosition
    {
        get
        {
            return chav.GetComponent<Transform>().position;
        }
    }

    public Toucher _hitBoxHead { get; private set; }


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
        gunshotWound = Instantiate(_gunshotWoundPrefab[UnityEngine.Random.Range(0, _gunshotWoundPrefab.Length)], splatterPosition, Quaternion.identity);
        gunshotWound.transform.SetParent(this.GetComponentInParent<Transform>());
    }


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
        var spriteRenderer = chav.GetComponent<SpriteRenderer>();

        shot++;
        if (shot % 2 == 0)
            spriteRenderer.sprite = _loader.spriteDic["Chav_attack"];
        else
            spriteRenderer.sprite = _loader.spriteDic["Chav"];
    }
}