using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteMaker : MonoBehaviour
{
    public List<Texture2D> textureArray = new List<Texture2D>();
    SpriteRenderer renderer;
    Texture2D texture;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        texture = MakeTexture(textureArray);

        renderer.sprite = MakeSprite(texture);
    }

    public void UpdateDrawing(Texture2D addedTexture, Vector2 clickPosition)
    {
        Vector2 relativePosition = TranslateWordPositionToTexturePosition(clickPosition);
        texture = AddTextureWithCoordinates(texture, addedTexture, (int)relativePosition.x, (int)relativePosition.y, textureArray);
        renderer.sprite = MakeSprite(texture);
    }

    public void ResetTexture()
    {
        for (int i = textureArray.Count - 1; i > 0; i --)
        {
            textureArray.RemoveRange(1, textureArray.Count - 1);
        }
        texture = MakeTexture(textureArray);
        renderer.sprite = MakeSprite(texture);
    }
    private Texture2D AddTextureWithCoordinates(Texture2D texture, Texture2D addedTexture, int posX, int posY, List<Texture2D> layers)
    {
        layers.Add(addedTexture);
        Texture2D newTexture = new Texture2D(layers[0].width, layers[0].height);
        int newX = (posX > addedTexture.width / 2) ? posX - addedTexture.width / 2 : 0;
        int newY = (posY > addedTexture.height / 2) ? posY - addedTexture.height / 2 : 0;
        int setWidth, setHeight;
        setWidth = (addedTexture.width + newX < newTexture.width) ? addedTexture.width : newTexture.width - newX;
        setHeight = (addedTexture.height + newY < newTexture.height) ? addedTexture.height : newTexture.height - newY;
        Color[] previousTexture = texture.GetPixels();
        Color[] getPixels = addedTexture.GetPixels(0, 0, addedTexture.width, addedTexture.height);
        Texture2D sizedLayer = ClearTexture(layers[0].width, layers[0].height);    
        sizedLayer.SetPixels(newX, newY, setWidth, setHeight, getPixels);
        Color[] adjustedLayers = sizedLayer.GetPixels();

        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                int pixelIndex = x + (y * newTexture.width);
                for (int i = 0; i < layers.Count; i++)
                {
                    Color sourcePixel = adjustedLayers[pixelIndex];

                    if (sourcePixel.a > 0 && previousTexture[pixelIndex].a > 0)
                    {
                        previousTexture[pixelIndex] = NormalBlend(previousTexture[pixelIndex], sourcePixel);
                    }
                }
            }
        }

        newTexture.SetPixels(previousTexture);
        newTexture.Apply();

        newTexture.wrapMode = TextureWrapMode.Clamp;

        return newTexture;
    }
    private Vector2 TranslateWordPositionToTexturePosition(Vector2 clickPosition)
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 planeMin = Camera.main.WorldToScreenPoint(transform.GetComponentInChildren<SpriteRenderer>().bounds.min);
        Vector2 planeMax = Camera.main.WorldToScreenPoint(transform.GetComponentInChildren<SpriteRenderer>().bounds.max);
        float xProportion = Mathf.InverseLerp(planeMin.x, planeMax.x, clickPosition.x);
        float yProportion = Mathf.InverseLerp(planeMin.y, planeMax.y, screenSize.y - clickPosition.y);
        float xPoint = xProportion * textureArray[0].width;
        float yPoint = yProportion * textureArray[0].height;
        return new Vector2(xPoint, yPoint);
    }

    private Texture2D MakeTexture(List<Texture2D> layers)
    {
        if (layers.Count == 0)
        {
            Debug.LogError("No image layer information in array!");
            return Texture2D.whiteTexture;
        }
        Texture2D newTexture = new Texture2D(layers[0].width, layers[0].height);
        Color[] colorArray = new Color[newTexture.width * newTexture.height];

        Color[][] adjustedLayers = new Color[layers.Count][];

        for (int i = 0; i < layers.Count; i++)
        {
            if (i == 0 || layers[i].width == newTexture.width && layers[i].height == newTexture.height)
            {
                adjustedLayers[i] = layers[i].GetPixels();
            }
            else
            {
                int getX, getWidth, setX, setWidth;
                getX = (layers[i].width > newTexture.width) ? (layers[i].width - newTexture.width) / 2 : 0;
                getWidth = (layers[i].width > newTexture.width) ? newTexture.width : layers[i].width;
                setX = (layers[i].width < newTexture.width) ? (newTexture.width - layers[i].width) / 2 : 0;
                setWidth = (layers[i].width < newTexture.width) ? layers[i].width : newTexture.width;
                int getY, getHeight, setY, setHeight;
                getY = (layers[i].height > newTexture.height) ? (layers[i].height - newTexture.height) / 2 : 0;
                getHeight = (layers[i].height > newTexture.height) ? newTexture.height : layers[i].height;
                setY = (layers[i].height < newTexture.height) ? (newTexture.height - layers[i].height) / 2 : 0;
                setHeight = (layers[i].height < newTexture.height) ? layers[i].height : newTexture.height;
                Color[] getPixels = layers[i].GetPixels(getX, getY, getWidth, getHeight);
                if (layers[i].width > newTexture.width && layers[i].height >= newTexture.height)
                {
                    adjustedLayers[i] = getPixels;
                }
                else
                {
                    Texture2D sizedLayer = ClearTexture(newTexture.width, newTexture.height);
                    sizedLayer.SetPixels(setX, setY, setWidth, setHeight, getPixels);
                    adjustedLayers[i] = sizedLayer.GetPixels();
                }
            }
        }

        return MergeTextures(layers, newTexture, colorArray, adjustedLayers);
    }

    private Texture2D MergeTextures(List<Texture2D> layers, Texture2D newTexture, Color[] colorArray, Color[][] adjustedLayers)
    {
        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                int pixelIndex = x + (y * newTexture.width);
                for (int i = 0; i < layers.Count; i++)
                {
                    Color sourcePixel = adjustedLayers[i][pixelIndex];


                    if (sourcePixel.a == 1)
                    {
                        colorArray[pixelIndex] = sourcePixel;
                    }
                    else if (sourcePixel.a > 0)
                    {
                        colorArray[pixelIndex] = NormalBlend(colorArray[pixelIndex], sourcePixel);
                    }
                }
            }
        }
        newTexture.SetPixels(colorArray);
        newTexture.Apply();

        newTexture.wrapMode = TextureWrapMode.Clamp;

        return newTexture;
    }

    private Texture2D ClearTexture(int width, int height)
    {
        Texture2D clearTexture = new Texture2D(width, height);
        Color[] clearPixels = new Color[width * height];
        clearTexture.SetPixels(clearPixels);
        return clearTexture;
    }

    private Color NormalBlend(Color destination, Color source)
    {
        float sourceAlpha = source.a;
        float destinationAlpha = (1 - sourceAlpha) * destination.a;
        Color destinationLayer = destination * destinationAlpha;
        Color sourceLayer = source * sourceAlpha;
        return destinationLayer + sourceLayer;
    }
    public Sprite MakeSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
    }
}
