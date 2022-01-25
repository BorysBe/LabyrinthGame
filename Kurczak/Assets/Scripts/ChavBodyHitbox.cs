using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChavBodyHitbox : Toucher
{
    [SerializeField] int hitDamage = 20;
    EnemyLifeCycle enemyHealth;
    [SerializeField] GameObject woundArea;

    private void Start()
    {
        enemyHealth = this.transform.root.GetComponent<EnemyLifeCycle>();
    }

    protected override void ActionOnTouch(PointerEventData eventData)
    {
        FindObjectOfType<Audio>().Play("PlayerShoot");
        enemyHealth.Damage(hitDamage);
        woundArea.GetComponent<ImageFactory>().SpriteDrawer(this.LastMousePosition);
        // bleedAnimation.StartAt(this.LastTouchPoint);
    }
}

