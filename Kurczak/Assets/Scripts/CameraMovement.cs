using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform _cameraPositionX;
    [SerializeField] [Range(0, 1)] float movementSpeed = 1;

    // Start is called before the first frame update
    void Awake()
    {
        _cameraPositionX = GetComponentInParent<Transform>();
    }


    public void Move(float value)
    {
        if(_cameraPositionX.position.x >= 56.82f)
        {
            _cameraPositionX.position = new Vector3(-56.81f, 0, -10);
        }
        if(_cameraPositionX.position.x <= -56.82f)
        {
            _cameraPositionX.position = new Vector3(56.81f, 0, -10);
        }
        else
        _cameraPositionX.position += new Vector3(value * Time.deltaTime * 10 * movementSpeed, 0, 0);
    }
}
