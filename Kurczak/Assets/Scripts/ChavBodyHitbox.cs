using UnityEngine;
using UnityEngine.EventSystems;

public class ChavBodyHitbox : Toucher
{

    [SerializeField] int hitDamage = 20;
    EnemyHealth enemyHealth;

    private void Start()
    {
        enemyHealth = this.transform.parent.parent.GetComponent<EnemyHealth>();
    }

    protected override void ActionOnTouch(PointerEventData eventData)
    {
        enemyHealth.Damage(hitDamage);
        enemyHealth.DrawInjuries(LastTouchPoint);
    }
}

