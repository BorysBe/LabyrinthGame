using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherEnemyPathing : MonoBehaviour
{


    void Update()
    {
        MoveForward();
        MoveSin();
    }

    private void MoveForward()
    {
        Vector2 pos = transform.position;

        pos.x += 5f * Time.deltaTime;

        if(pos.x > 10)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }

    private void MoveSin()
    {
        Vector2 pos = transform.position;

        float sin = Mathf.Sin(pos.x);
        pos.y = sin;
    }
}
