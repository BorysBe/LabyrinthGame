using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteCommand : IChangeSpriteCommand
{
    private readonly List<GameObject> _sprites;
    bool firstFrameActive = false;
    private int spriteIndex = 0;
    private MonoBehaviourUpdateTimer _timer;

    private Action ResetSpriteIndexAction { get; set; }

    public ChangeSpriteCommand(List<GameObject> sprites, MonoBehaviourUpdateTimer timer, bool isLooped, bool firstFrameActive )
    {
        this.firstFrameActive = firstFrameActive;
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
                return;
            }
            _sprites[spriteIndex].SetActive(false);
            spriteIndex++;
            _sprites[spriteIndex].SetActive(true);
        };
        this._sprites = sprites;
        DeactivateAllFrames();
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
        _sprites[spriteIndex].SetActive(firstFrameActive);
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

