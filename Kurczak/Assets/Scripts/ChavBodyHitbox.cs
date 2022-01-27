using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChavBodyHitbox : Toucher
{
    [SerializeField] int hitDamage = 20;
    EnemyLifeCycle enemyHealth;
    [SerializeField] GameObject woundArea;
    List<GameObject> animations;

    private void Start()
    {
        enemyHealth = this.transform.root.GetComponent<EnemyLifeCycle>();
    }

    protected override void ActionOnTouch(PointerEventData eventData)
    {
        FindObjectOfType<Audio>().Play("PlayerShoot");
        animations = transform.root.GetComponent<CharacterStateAnimation>().attachedAnimations;
        woundArea.GetComponent<SpriteRendererDrawer>().DrawSprite(this.LastMousePosition);
        var blood = EnemyFactory.Instance.Spawn(PrefabType.BloodSpring, this.LastTouchPoint, transform.root);
        blood.Play();
        enemyHealth.Damage(hitDamage);
    }
}

