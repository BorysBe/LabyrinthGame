using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatingSatanistBodyHitbox : Toucher
{
    [SerializeField] int hitDamage;
    RotatingSatanistLifeCycle enemyHealth;

    private void Start()
    {
        enemyHealth = this.transform.root.GetComponent<RotatingSatanistLifeCycle>();
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

