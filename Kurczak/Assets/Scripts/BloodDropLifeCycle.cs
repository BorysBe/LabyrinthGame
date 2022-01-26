using System;

public class BloodDropLifeCycle : ObjectLifeCycle, IPlayable
{
    public override void Play()
    {
        this.transform.tag = "Busy";
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
