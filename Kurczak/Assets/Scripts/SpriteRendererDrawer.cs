using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRendererDrawer: MonoBehaviour
{
    TextureFactory textureFactory;
    SpriteRenderer renderer;
    [SerializeField] Vector2Int drawAreaParameters;
    Texture2D drawArea;
    [SerializeField] Texture2D[] textures;

    void Start()
    {
        textureFactory = new TextureFactory(textures);
        renderer = GetComponent<SpriteRenderer>();
        drawArea = new Texture2D(drawAreaParameters.x, drawAreaParameters.y);
        renderer.sprite = textureFactory.CreateEmpty(drawArea);
    }

    public void DrawSprite(Vector3 lastTouchPoint)
    {
        Vector2 relativePosition = TranslateWordPositionToTexturePosition(lastTouchPoint);
        var sprite = textureFactory.CreateRandom();
        Color[] getPixels = sprite.GetPixels(0, 0, sprite.width, sprite.height);
        renderer.sprite.texture.SetPixels((int)relativePosition.x, (int)relativePosition.y, sprite.width, sprite.height, getPixels);
        renderer.sprite.texture.Apply();
    }

    public void DrawSpriteWithDefiniedSprite(Vector3 lastTouchPoint, Texture2D texture)
    {
        Vector2 relativePosition = TranslateWordPositionToTexturePosition(lastTouchPoint);
        var sprite = texture;
        Color[] getPixels = sprite.GetPixels(0, 0, sprite.width, sprite.height);
        renderer.sprite.texture.SetPixels((int)relativePosition.x, (int)relativePosition.y, sprite.width, sprite.height, getPixels);
        renderer.sprite.texture.Apply();
    }

    public void ClearImage()
    {
        drawArea = new Texture2D(drawAreaParameters.x, drawAreaParameters.y);
        renderer.sprite = textureFactory.CreateEmpty(drawArea);
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

}
