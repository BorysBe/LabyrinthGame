using System;
using UnityEngine;
using System.Collections;

public class Chavs : MonoBehaviour
{
    private State _state;

    public EnemyHealthbar _healthbar;
    public Action ActiveDrawing { get; private set; }
    public Action ActiveSound { get; private set; }

    private AtlasLoader _loader;

    public int health = 200;
    int currentHealth;

    public GameObject _chavGrave;
    public GameObject _bloodyExplosion;

    // Start is called before the first frame update
    void Start()
    {
        _loader = new AtlasLoader(@"Sprites\Enemies");
        SwitchStateTo(State.Move);
        var hitboxCanvas = GameObject.FindGameObjectWithTag("Hitbox Canvas");
        var chavToucherBody = GameObject.FindGameObjectWithTag("ChavToucherBody");
        var chavToucherHead = GameObject.FindGameObjectWithTag("ChavToucherHead");
        chavToucherBody.GetComponent<ChavBodyHitbox>().enabled = true;
        chavToucherHead.GetComponent<ChavBodyHitbox>().enabled = true;
        currentHealth = health;
        _healthbar.SetHealth(currentHealth, health);
    }

    // Update is called once per frame
    void Update()
    {
        DrawChavs();
        SetPosition();
        if (currentHealth <= 0)
        {
            SwitchStateTo(State.Dying);
        }
    }

    private void SetPosition()
    {
        var cam = Camera.main;
        Vector3 point = new Vector3();
        point = cam.WorldToScreenPoint(this.WorldPosition);

        //Debug.Log("Chaw position: " + point.ToString("F3"));
        var chavToucherBody = GameObject.FindGameObjectWithTag("ChavToucherBody");

        chavToucherBody.transform.position = new Vector3(point.x, point.y, point.z);

        var chavToucherHead = GameObject.FindGameObjectWithTag("ChavToucherHead");

        chavToucherHead.transform.position = new Vector3(point.x, point.y, point.z);
    }

    public Vector3 WorldPosition
    {
        get
        {
            var chav = GameObject.FindGameObjectWithTag("Chav");
            return chav.GetComponent<Transform>().position;
        }
    }

    public Toucher _hitBoxHead { get; private set; }

    private void DrawChavs()
    {
        ActiveDrawing?.Invoke();
    }

    public void Damage(int value)
    {
        currentHealth -= value;
        _healthbar.SetHealth(currentHealth, health);
    }


    public enum State
    {
        Move,
        Shooting,
        Dying
    }

    public void SwitchStateTo(State newState)
    {
        _state = newState;
        switch (_state)
        {
            case State.Move:
                ActiveDrawing = delegate ()
                {
                    var chav = GameObject.FindGameObjectWithTag("Chav");
                    var spriteRenderer = chav.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = _loader.spriteDic["Chav"];
                    DrawInjuries();
                };
                ActiveSound = delegate { };
                break;
            case State.Dying:
                ActiveDrawing = delegate ()
                {
                    var chav = GameObject.FindGameObjectWithTag("Chav");
                    Vector3 instantionPosition = chav.transform.position;
                    Vector3 instantionPositionLeft = new Vector3(chav.transform.position.x - 2f, chav.transform.position.y, chav.transform.position.z);
                    Vector3 instantionPositionRight = new Vector3(chav.transform.position.x + 2f, chav.transform.position.y, chav.transform.position.z);
                    Instantiate(_bloodyExplosion, instantionPosition, Quaternion.identity);
                    Instantiate(_chavGrave, instantionPosition, Quaternion.identity);
                    var chavToucherBody = GameObject.FindGameObjectWithTag("ChavToucherBody");
                    var chavToucherHead = GameObject.FindGameObjectWithTag("ChavToucherHead");
                    chavToucherBody.GetComponent<ChavBodyHitbox>().enabled = false;
                    chavToucherHead.GetComponent<ChavBodyHitbox>().enabled = false;
                    Destroy(chav);
                };
                ActiveSound = delegate { };

                break;
            case State.Shooting:
                ActiveDrawing = delegate ()
                {
                    var chav = GameObject.FindGameObjectWithTag("Chav");
                    var spriteRenderer = chav.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = _loader.spriteDic["Chav_attack"];
                    DrawInjuries();
                };

                ActiveSound = delegate { };

                break;
            default:
                break;
        }
    }

    private void DrawInjuries()
    {
        //
    }
}