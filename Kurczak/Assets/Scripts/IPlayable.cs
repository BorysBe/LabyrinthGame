

using System;

public interface IPlayable
{
    void Play();

    void Stop();

    Action OnFinish { get; set; }
}
