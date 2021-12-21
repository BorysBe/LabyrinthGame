using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private Player _controls;
    private float _mouseSensitivity = 100f;
    private Vector2 _mouseLook;
    private float _xRotation = 0f;
    private Transform _playerBody;

    private void Awake()
    {
        _playerBody = transform.parent;

        _controls = new Player();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        Time.timeScale = 1f; //nie wiem dlaczego tak ma byæ, ale rozwi¹zuje problem buguj¹cego siê rozgladania po ponownym uruchomieniu gry z menu
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        _mouseLook = _controls.PlayerMain.Look.ReadValue<Vector2>();

        float mouseX = _mouseLook.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = _mouseLook.y * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _playerBody.Rotate(Vector3.up * mouseX);
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
