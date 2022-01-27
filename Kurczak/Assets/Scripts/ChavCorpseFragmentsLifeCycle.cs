using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChavCorpseFragmentsLifeCycle : ObjectLifeCycle, IPlayable
{
    public override void Play()
    {
        var animation = GetComponent<RemainsSpawner>();
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
