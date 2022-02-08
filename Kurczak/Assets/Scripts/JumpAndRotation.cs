using System;
using UnityEngine;

public class JumpAndRotation : MonoBehaviour, IPlayable
{
    [SerializeField] [Range(0, 1)] float opacityChangedInTime = 1f;
    [SerializeField] Vector3 rotationDirection = new Vector3(0f, 0f, 20f);
    [SerializeField] float rotationsPerSecond = 1f;
    [SerializeField] GameObject rotatingObject;
    [SerializeField] float positionY = 0f;
    public bool transitionActivated;
    RotatePerSecond rotate;

    float speedController = 1f;
    int timeToReset = 1000;

    private Action RotateAction { get; set; }

    private Action Move { get; set; }

    public Action OnFinish { get; set; }

    public void Start()
    {
        transitionActivated = false;
        rotate = this.GetComponent<RotatePerSecond>();
        RotateAction = NullObjectMove;
        Move = NullObjectMove;
    }
    void Update()
    {
        Move.Invoke();
        RotateAction.Invoke();
    }
    public void Play()
    {
        transitionActivated = true;
        ReverseVectorCoroutineTimer();
        Move = MoveUpwards;
        RotateAction = Rotate;
    }

    public void Stop()
    {
        Move = NullObjectMove;
        RotateAction = NullObjectMove;
    }

    void MoveUpwards()
    {
        positionY += speedController * Time.deltaTime;
        this.transform.Translate(0, positionY, 0);
    }

    void MoveDown()
    {
        positionY += speedController * Time.deltaTime;
        this.transform.Translate(0, - positionY, 0);
    }

    void UntilStopCoroutineTimer()
    {
        CoroutineTimer coroutineTimer = new CoroutineTimer(timeToReset, this);
        coroutineTimer.Tick += delegate ()
        {
            Stop();
            coroutineTimer.Stop();
        };
        coroutineTimer.Play();
    }

    void ReverseVectorCoroutineTimer()
    {
        CoroutineTimer coroutineTimer = new CoroutineTimer(timeToReset, this);
        coroutineTimer.Tick += delegate ()
        {
            positionY = 0;
            Move = MoveDown;
            UntilStopCoroutineTimer();
            coroutineTimer.Stop();
        };
        coroutineTimer.Play();
    }

    private void NullObjectMove()
    {
        // intentionally left blank
    }

    void Rotate()
    {
        rotate.Rotate(rotationDirection, rotationsPerSecond, rotatingObject);
    }

}
