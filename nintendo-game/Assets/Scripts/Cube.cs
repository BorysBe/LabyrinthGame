using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate(new Vector3(4,5,6));
    }
    void Rotate(Vector3 vector)
    {
        transform.RotateAround(transform.position, Vector3.forward, 45);
    }
}
