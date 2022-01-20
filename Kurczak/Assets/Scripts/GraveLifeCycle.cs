using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveLifeCycle : ObjectLifeCycle, IPlayable
{
    public override void Play()
    {

    }

    public override void Stop()
    {
        base.Stop();
    }
}
