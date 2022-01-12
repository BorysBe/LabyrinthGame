using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    private CharacterController _characterController;
    private Controller _controller;
    private Vector2 move;

    private void Awake()
    {
        _controller = new Controller();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _controller.Enable();
    }

    //private void OnDisable()
    //{
    //    _controller.Disable();
    //}

    void Update()
    {
        Moving();
    }

    public void Moving()
    {
        move = _controller.Slider.Move.ReadValue<Vector2>();
        Vector3 movement = move.x * transform.right;
        _characterController.Move(movement * moveSpeed * Time.deltaTime);
    }
}
