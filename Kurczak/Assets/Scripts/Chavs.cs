using System;
using UnityEngine;

public class Chavs : MonoBehaviour
{
    private State _state;

    public Action ActiveDrawing { get; private set; }
    public Action ActiveSound { get; private set; }

    private AtlasLoader _loader;

    public int health = 200;

    public GameObject _myHitbox;

    // Start is called before the first frame update
    void Start()
    {
        _loader = new AtlasLoader(@"Sprites\Enemies");
        SwitchStateTo(State.Move);
        var hitboxCanvas = GameObject.FindGameObjectWithTag("Hitbox Canvas");
        Instantiate(_myHitbox, new Vector3(0, 0, 0), Quaternion.identity);
        _myHitbox.transform.SetParent(hitboxCanvas.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        DrawChavs();
        SetPosition();
        if (health <= 0)
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
        var chavToucher = GameObject.FindGameObjectWithTag("ChavToucher");

        chavToucher.transform.position = new Vector3(point.x, point.y, point.z);
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
        health -= value;
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
                ActiveDrawing = delegate () {
                    var chav = GameObject.FindGameObjectWithTag("Chav");
                    var spriteRenderer = chav.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = _loader.spriteDic["Chav"];
                    DrawInjuries();
                };
                ActiveSound = delegate { };
                break;
            case State.Dying:
                ActiveDrawing = delegate () {
                    var chav = GameObject.FindGameObjectWithTag("Chav");
                    var spriteRenderer = chav.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = _loader.spriteDic["Chav"];
                };
                ActiveSound = delegate { };

                break;
            case State.Shooting:
                ActiveDrawing = delegate () {
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
