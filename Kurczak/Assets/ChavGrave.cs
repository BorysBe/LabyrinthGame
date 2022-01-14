using UnityEngine;

public class ChavGrave : MonoBehaviour
{
    public OffsetAndOpacity offsetAndOpacity;
    int timeToReset = 3000;

    public void Play()
    {
        offsetAndOpacity.enabled = true;
    }

    public void ResetOpacity(CoroutineTimer coroutineTimer)
    {
        offsetAndOpacity.OpacityReset();
        offsetAndOpacity.enabled = false;
    }

    void CountTimeToReset()
    {
        CoroutineTimer coroutineTimer = new CoroutineTimer(timeToReset, this);
        coroutineTimer.Tick += delegate ()
        {
            ResetOpacity(coroutineTimer);
        };
        coroutineTimer.Play();
    }
}
