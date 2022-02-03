using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MotorcycleBodyHitbox : Toucher
{
    [SerializeField] int hitDamage;
    MotorcycleLifeCycle enemyHealth;
    [SerializeField] GameObject woundArea;

    private void Start()
    {
        enemyHealth = this.transform.root.GetComponent<MotorcycleLifeCycle>();
    }

    protected override void ActionOnTouch(PointerEventData eventData)
    {
        FindObjectOfType<Audio>().Play("PlayerShoot");
        woundArea.GetComponent<SpriteRendererDrawer>().DrawSprite(this.LastMousePosition);
        var blood = EnemyFactory.Instance.Spawn(PrefabType.BloodSpring, this.LastTouchPoint, transform.root);
        blood.Play();
        enemyHealth.Damage(hitDamage);
    }
}

