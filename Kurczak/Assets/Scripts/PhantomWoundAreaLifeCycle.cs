public class PhantomWoundAreaLifeCycle : ObjectLifeCycle, IPlayable
{
    public override void Play()
    {
        //Intentionally left blank.
    }

    public override void Stop()
    {
        base.Stop();
        Destroy(this.gameObject);
    }
}
