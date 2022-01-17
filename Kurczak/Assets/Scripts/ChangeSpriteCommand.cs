using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteCommand : IChangeSpriteCommand
{
    private readonly List<GameObject> _sprites;
    private int spriteIndex = 0;
    private MonoBehaviourUpdateTimer _timer;

    private Action ResetSpriteIndexAction { get; set; }

    public Action OnFinish;

    public ChangeSpriteCommand(List<GameObject> sprites, MonoBehaviourUpdateTimer timer, bool isLooped)
    {
        if (isLooped)
            ResetSpriteIndexAction = ResetSpriteIndex;
        else
            ResetSpriteIndexAction = NullResetSpriteIndex;

        _timer = timer;
        _timer.Tick += delegate ()
        {
            if (spriteIndex == _sprites.Count - 1)
            {
                ResetSpriteIndexAction.Invoke();
                if (!isLooped)
                {
                    _timer.Stop();
                    OnFinish?.Invoke();
                }
                return;
            }
            _sprites[spriteIndex].SetActive(false);
            spriteIndex++;
            _sprites[spriteIndex].SetActive(true);
        };
        this._sprites = sprites;
        ForceReset();
    }

    public bool CanExecute()
    {
        return _timer.IsEnabled;
    }

    public void Execute()
    {
        _timer.Start();
    }

    private void NullResetSpriteIndex()
    {
         _timer.Stop();
        ForceReset();
    }

    private void ResetSpriteIndex()
    {
        ForceReset();
    }

    public void ForceReset()
    {
        DeactivateAllFrames();
        _sprites[spriteIndex].SetActive(true);
    }

    private void DeactivateAllFrames()
    {
        foreach (GameObject s in _sprites)
        {
            s.SetActive(false);
        }
        spriteIndex = 0;
    }
}

