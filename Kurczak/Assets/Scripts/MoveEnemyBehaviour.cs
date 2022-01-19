using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyBehaviour : MonoBehaviour, IPlayable
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] GameObject enemyPath;
    private Vector3 deafaultPosition;

    private Action MoveAction { get; set; }
    public Action OnFinish { get; set; }

    List<Transform> waypoints;
    int waypointIndex = 0;

    void Start()
    {
        waypoints = GetWaypoints();
        MoveAction = NullObjectMove;
    }

    public void Play()
    {
        deafaultPosition = transform.position;
        MoveAction = Move;
    }

    public void Stop()
    {
        waypointIndex = 0;
        MoveAction = NullObjectMove;
        OnFinish?.Invoke();
        StopAllAnimations();
    }

    private void StopAllAnimations()
    {
        var stoppable = new List<IPlayable> {
            GetComponent<OneTimeAnimation>(),
            GetComponent<LoopAnimation>(),
            GetComponent<OneTimeAnimationComposite>(),
            GetComponent<EnemyLifeCycle>()
        };
        foreach (var anim in stoppable)
        {
            anim.Stop();
        }
    }

    private void SetDefaultPosition()
    {
        transform.position = deafaultPosition;
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
        if (waypointIndex == 0)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            this.transform.position = targetPosition;
        }
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            this.GetComponent<Animator>().SetBool("ReturnToIdleState", true);
            Stop();
        }

    }
}
