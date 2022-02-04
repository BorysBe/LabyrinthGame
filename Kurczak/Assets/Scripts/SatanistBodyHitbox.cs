using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SatanistBodyHitbox : Toucher
{
    [SerializeField] int hitDamage;
    SatanistLifeCycle enemyHealth;

    private void Start()
    {
        enemyHealth = this.transform.root.GetComponent<SatanistLifeCycle>();
    }

    protected override void ActionOnTouch(PointerEventData eventData)
    {
        FindObjectOfType<Audio>().Play("PlayerShoot");
        var woundArea = transform.Find("SatanistWoundArea(Clone)");
        woundArea.GetComponent<SpriteRendererDrawer>().DrawSprite(this.LastMousePosition);
        var blood = EnemyFactory.Instance.Spawn(PrefabType.BloodSpring, this.LastTouchPoint, transform.root);
        blood.Play();
        enemyHealth.Damage(hitDamage);
    }
}

