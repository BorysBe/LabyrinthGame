using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private CharacterController _controller;
    private Player _controls;
    private bool isGrounded;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private Vector2 move;

    public Transform ground;
    public float distanceToGround = 0.01f;
    public LayerMask groundMask;

    private void Awake()
    {
        _controls = new Player();
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        PlayerMovement();
        Gravity();
    }

    private void PlayerMovement()
    {
        move = _controls.PlayerMain.Move.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        _controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);
    }

}