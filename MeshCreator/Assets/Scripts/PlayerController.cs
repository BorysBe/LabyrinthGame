using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    private Player _controls;
    private bool isGrounded;
    private float moveSpeed = 4f;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private Vector2 move;

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
    }

    private void PlayerMovement()
    {
        move = _controls.PlayerMain.Move.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        _controller.Move(movement * moveSpeed * Time.deltaTime);
    }

}