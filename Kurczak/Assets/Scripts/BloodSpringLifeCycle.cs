public class BloodSpringLifeCycle : ObjectLifeCycle, IPlayable
{
    public override void Play()
    {
        var animation = GetComponent<OneTimeAnimation>();
        animation.OnFinish += delegate ()
        {
            this.OnFinish?.Invoke();
        };
    }

    public override void Stop()
    {
        transform.parent = null;
        base.Stop();
    } 
}
