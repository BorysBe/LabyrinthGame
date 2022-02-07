using UnityEngine;
using UnityEngine.EventSystems;

public class PhantomBodyHitbox : Toucher
{
    [SerializeField] int hitDamage;
    PhantomLifeCycle enemyHealth;

    private void Start()
    {
        enemyHealth = this.transform.root.GetComponent<PhantomLifeCycle>();
    }

    protected override void ActionOnTouch(PointerEventData eventData)
    {
        FindObjectOfType<Audio>().Play("PlayerShoot");
        var woundArea = transform.Find("PhantomWoundArea(Clone)");
        woundArea.GetComponent<SpriteRendererDrawer>().DrawSprite(this.LastMousePosition);
        var blood = EnemyFactory.Instance.Spawn(PrefabType.BloodSpring, this.LastTouchPoint, transform.root);
        blood.Play();
        enemyHealth.Damage(hitDamage);
    }
}

