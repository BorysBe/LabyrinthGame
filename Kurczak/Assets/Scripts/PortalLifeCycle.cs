public class PortalLifeCycle : ObjectLifeCycle, IPlayable
{
    public override void Play()
    {
        var animation = GetComponentInParent<OneTimeAnimation>();
        animation.OnFinish += delegate ()
        {

            this.OnFinish?.Invoke();
        };
    }

    public override void Stop()
    {
        base.Stop();
    }
}
