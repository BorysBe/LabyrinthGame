using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Controller _controller;
    private CameraMovement _cameraMovement;

    private void Awake()
    {
        _controller = new Controller();
        _cameraMovement =  GetComponent<CameraMovement>();
    }

    private void OnEnable()
    {
        _controller.Enable();
    }

    private void OnDisable()
    {
        _controller.Disable();
    }

    void Update()
    {
        _cameraMovement.Move(ReadMovementFromInput());

    }

    public float ReadMovementFromInput()
    {
        return (int)_controller.Slider.Move.ReadValue<float>();
    }
}
