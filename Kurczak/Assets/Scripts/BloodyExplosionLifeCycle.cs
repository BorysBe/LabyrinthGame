using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyExplosionLifeCycle : ObjectLifeCycle, IPlayable
{
    public override void Play()
    {
        var animation = GetComponentInParent<OneTimeAnimationComposite>();
        animation.OnFinish += delegate()
        {

            this.OnFinish?.Invoke();
        };
    }

    public override void Stop()
    {
        base.Stop();
    }
}
