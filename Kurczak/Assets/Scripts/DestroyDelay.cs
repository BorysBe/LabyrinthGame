using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    [SerializeField] float delay = 5f;

    public void Start()
    {
        Destroy(this.gameObject, delay);
    }


}
