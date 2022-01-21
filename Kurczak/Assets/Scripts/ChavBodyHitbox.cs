using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChavBodyHitbox : Toucher
{

    [SerializeField] int hitDamage = 20;
    EnemyLifeCycle enemyHealth;
    public List<SpriteMaker> enemySprites = new List<SpriteMaker>();
    GetRandomTexture random;

    private void Start()
    {
        enemyHealth = this.transform.root.GetComponent<EnemyLifeCycle>();
        random = transform.root.GetComponent<GetRandomTexture>();

    }

    protected override void ActionOnTouch(PointerEventData eventData)
    {
        ////Get the screen size
        ////Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        ////Get the texture size
        //Vector2 textureSize = new Vector2(texture.width, texture.height);
        ////Get the screen position of the texture (This will be the center of the image)
        //Vector2 textureScreenPosition = Camera.main.WorldToScreenPoint(screenObject.transform.position);
        ////Get the 0,0 position of the texture:
        //Vector2 textureStartPosition = textureScreenPosition - textureSize / 2;
        ////Subtract the 0,0 position of the texture from the mouse click position.
        //Vector2 relativeClickPosition = mouseClickPosition - textureStartPosition;

        FindObjectOfType<Audio>().Play("PlayerShoot");
        enemyHealth.Damage(hitDamage);
        foreach (var s in enemySprites)
        {
            s.UpdateDrawing(random.ReturnTexture(), LastMousePosition);
        }
    }
}

