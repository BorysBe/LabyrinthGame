using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteCommand : IChangeSpriteCommand
{
    private readonly List<GameObject> _sprites;
    private int spriteIndex = 0;
    private MonoBehaviourUpdateTimer _timer;

    public Action ResetSpriteIndexAction { get; private set; }

    public ChangeSpriteCommand(List<GameObject> sprites, MonoBehaviourUpdateTimer timer, bool isLooped)
    {
        if (isLooped)
            ResetSpriteIndexAction = ResetSpriteIndex;
        else
            ResetSpriteIndexAction = NullResetSpriteIndex;

        _timer = timer;

        _timer.Tick += delegate ()
        {
            spriteIndex++;
            _sprites[spriteIndex - 1].SetActive(false);
        };

        this._sprites = sprites;
        foreach (GameObject s in _sprites)
        {
            s.SetActive(false);
        }
        _sprites[0].SetActive(true);
        _timer.Start();
    }


    public bool CanExecute()
    {
        return spriteIndex < _sprites.Count;
    }

    public void Execute()
    {
        if (CanExecute())
            _sprites[spriteIndex].SetActive(true);
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
}

