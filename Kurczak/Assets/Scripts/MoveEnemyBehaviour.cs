using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] GameObject enemyPath;
    private Action MoveAction { get; set; }

    List<Transform> waypoints;
    int waypointIndex = 0;

    void Start()
    {
        waypoints = GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
        MoveAction = NullObjectMove;
    }

    public void Play()
    {
        MoveAction = Move;
    }

    public void Stop()
    {
        MoveAction = NullObjectMove;
    }

    private List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in enemyPath.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    void Update()
    {
        MoveAction.Invoke();
    }

    private void NullObjectMove()
    {
        // intentionally left blank
    }

    private void Move()
    {
        
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var transform = this.GetComponentInParent<Transform>();
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
