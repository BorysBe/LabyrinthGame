using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ImageFactory: MonoBehaviour
{
    public List<Texture2D> wounds = new List<Texture2D>();
    SpriteRenderer renderer;
    [SerializeField] Vector2Int drawAreaParameters;
    Texture2D drawArea;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        drawArea = new Texture2D(drawAreaParameters.x, drawAreaParameters.y);
        renderer.sprite = CreateEmptySprite(drawArea);
    }

    public  void SpriteDrawer(Vector3 lastTouchPoint)
    {
        Vector2 relativePosition = TranslateWordPositionToTexturePosition(lastTouchPoint);
        var wound = wounds[0];
        List<Color> colors = new List<Color>();
        for (int idx = 0; idx < wound.width * wound.height; idx++)
            colors.Add(new Color(0, 1, 0));

        Color[] getPixels = wound.GetPixels(0, 0, wound.width, wound.height);
        renderer.sprite.texture.SetPixels((int)relativePosition.x, (int)relativePosition.y, wound.width, wound.height, getPixels);
        renderer.sprite.texture.Apply();
    }

    private Vector2 TranslateWordPositionToTexturePosition(Vector2 clickPosition)
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 planeMin = Camera.main.WorldToScreenPoint(transform.GetComponentInChildren<SpriteRenderer>().bounds.min);
        Vector2 planeMax = Camera.main.WorldToScreenPoint(transform.GetComponentInChildren<SpriteRenderer>().bounds.max);
        float xProportion = Mathf.InverseLerp(planeMin.x, planeMax.x, clickPosition.x);
        float yProportion = Mathf.InverseLerp(planeMin.y, planeMax.y, screenSize.y - clickPosition.y);
        float xPoint = xProportion * drawArea.width;
        float yPoint = yProportion * drawArea.height;
        return new Vector2(xPoint, yPoint);
    }

    private Sprite CreateEmptySprite(Texture2D texture)
    {
        var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        List<Color> colors = new List<Color>();
        for (int idx = 0; idx < texture.width * texture.height; idx++)
            colors.Add(new Color(0, 0, 0, 0));
        Color[] getPixels = colors.ToArray();
        texture.SetPixels(0, 0, texture.width, texture.height, getPixels);
        texture.Apply();
        return sprite;
    }
}
