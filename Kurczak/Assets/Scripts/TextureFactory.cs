using System.Collections.Generic;
using UnityEngine;

public class TextureFactory
{
    private Texture2D[] textures;
    public TextureFactory(Texture2D[] textures)
    {
        this.textures = textures;
    }

    public Texture2D CreateRandom()
    {
        return textures[Random.Range(0, textures.Length - 1)];
    }

    public Sprite CreateEmpty(Texture2D texture)
    {
        var sprite = CreateSprite(texture);
        List<Color> colors = new List<Color>();
        for (int idx = 0; idx < texture.width * texture.height; idx++)
            colors.Add(new Color(0, 0, 0, 0));
        Color[] getPixels = colors.ToArray();
        texture.SetPixels(0, 0, texture.width, texture.height, getPixels);
        texture.Apply();
        return sprite;
    }

    private Sprite CreateSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
    }
}
