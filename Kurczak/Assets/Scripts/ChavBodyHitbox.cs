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
        enemyHealth.Damage(hitDamage);
        woundArea.GetComponent<ImageFactory>().SpriteDrawer(this.LastMousePosition);
        var blood = EnemyFactory.Instance.Spawn("BloodDrop", this.LastTouchPoint, transform.root);
 //       blood.GameObject.transform.SetParent(transform.root);
        blood.Play();
    }
}

