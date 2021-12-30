using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] PlayerWeapon _damageDealer;
    public float xposition;
    public float yposition;


    // Start is called before the first frame update
    void Start()
    {
        _damageDealer = GetComponent<PlayerWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        xposition = transform.position.x;
        yposition = transform.position.y;
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            return;
        }

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        Debug.Log(touchPosition);

        if(touchPosition.x < transform.position.x + 100 && touchPosition.x > transform.position.x -100 && touchPosition.y < transform.position.y +100 && touchPosition.y > transform.position.y -100)
        {
            EnemyShooted();
        }
    }

    public void EnemyShooted()
    {

        Debug.Log("enemy is shooted");
    }
}
