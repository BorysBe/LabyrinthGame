using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteCommand : IChangeSpriteCommand
{
    private readonly List<GameObject> _sprites;
    private int spriteIndex = 0;
    private MonoBehaviourUpdateTimer _timer;

    private Action ResetSpriteIndexAction { get; set; }

    public ChangeSpriteCommand(List<GameObject> sprites, MonoBehaviourUpdateTimer timer, bool isLooped)
    {
        if (isLooped)
            ResetSpriteIndexAction = ResetSpriteIndex;
        else
            ResetSpriteIndexAction = NullResetSpriteIndex;

        _timer = timer;

        _timer.Tick += delegate ()
        {
            _sprites[spriteIndex].SetActive(false);
            _sprites[spriteIndex + 1].SetActive(true);
        };

        this._sprites = sprites;
        this.ForceReset();
        _timer.Start();
    }

    public bool CanExecute()
    {
        return spriteIndex < _sprites.Count;
    }

    public void Execute()
    {
        ResetSpriteIndexAction.Invoke();
    }

    private void NullResetSpriteIndex()
    {
        // intentionally left blank
    }

    private void ResetSpriteIndex()
    {
        if (spriteIndex == _sprites.Count - 1)
        {
            spriteIndex = 0;
        }
    }

    public void ForceReset()
    {
        foreach (GameObject s in _sprites)
        {
            s.SetActive(false);
        }
        spriteIndex = 0;
        _sprites[spriteIndex].SetActive(true);
    }
}

