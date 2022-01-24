using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChavBodyHitbox : Toucher
{

    [SerializeField] int hitDamage = 20;
    EnemyLifeCycle enemyHealth;
    public List<SpriteMaker> enemySprites = new List<SpriteMaker>();
    TextureFactory randomizer;

    private void Start()
    {
        enemyHealth = this.transform.root.GetComponent<EnemyLifeCycle>();
        randomizer = transform.root.GetComponent<TextureFactory>();

    }

    protected override void ActionOnTouch(PointerEventData eventData)
    {
        FindObjectOfType<Audio>().Play("PlayerShoot");
        enemyHealth.Damage(hitDamage);
        var randomizedWound = randomizer.CreateRandomTexture();
        foreach (var s in enemySprites)
        {
            s.UpdateDrawing(randomizedWound, LastMousePosition);
        }
    }
}

