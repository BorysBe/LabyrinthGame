using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveEnemyBehaviour : MonoBehaviour, IPlayable
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] GameObject enemyPath;
    [SerializeField] GameObject headHitbox;
    [SerializeField] GameObject bodyHitbox;
    [SerializeField] GameObject hitboxCanvas;
    private Vector3 deafaultPosition;

    private Action MoveAction { get; set; }
    public Action OnFinish { get; set; }

    List<Transform> waypoints;
    int waypointIndex = 0;
    bool reverseMovement = false;

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
        foreach (var anim in stoppable.Where(x => x != null))
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
            reverseMovement = false;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (waypointIndex <= waypoints.Count - 1 && !reverseMovement)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
                if (waypointIndex == waypoints.Count)
                {
                    reverseMovement = true;
                    transform.Rotate(0, 180, 0);
                }
            }
        }

        if (waypointIndex >= 0 && reverseMovement)
        {
            var targetPosition = waypoints[waypointIndex - 1].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            this.transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex--;
            }
        }
    }
}
