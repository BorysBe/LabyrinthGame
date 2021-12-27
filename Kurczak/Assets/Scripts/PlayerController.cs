using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Controller _controller;
    private BackgroundScroller _backgroundScroller;
    int movementInput = 0;

    private void Awake()
    {
        _controller = new Controller();
        _backgroundScroller = GetComponent<BackgroundScroller>();
    }

    private void OnEnable()
    {
        _controller.Enable();
    }

    private void OnDisable()
    {
        _controller.Disable();
    }

    void Start()
    {
        
    }


    void Update()
    {
        movementInput = (int)_controller.Slider.Move.ReadValue<float>();
        _backgroundScroller.OffsetUpdate(movementInput);
    }
}
