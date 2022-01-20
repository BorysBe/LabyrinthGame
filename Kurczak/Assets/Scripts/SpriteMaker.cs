using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteMaker : MonoBehaviour
{
    SpriteRenderer renderer;

    public Texture2D sourceTexture;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        Texture2D texture = new Texture2D(sourceTexture.width, sourceTexture.height);
        Color[] colorArray = new Color[texture.width * texture.height];

        Color[] sourceArray = sourceTexture.GetPixels();

        for (int x = 0; x < texture.width; x++)
        {
            for(int y = 0; y <texture.height; y++)
            {
                int pixelIndex = x + (y * texture.width);
                Color sourcePixel = sourceArray[pixelIndex];
                colorArray[pixelIndex] = sourcePixel;
            }
        }
        texture.SetPixels(colorArray);
        texture.Apply();

        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;

        Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        renderer.sprite = newSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
