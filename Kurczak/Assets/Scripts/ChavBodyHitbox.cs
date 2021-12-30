using UnityEngine;
using UnityEngine.EventSystems;

public class ChavBodyHitbox : Toucher
{
    [SerializeField] int hitDamage = 20;
    protected override void ActionOnTouch(PointerEventData eventData)
    {
        var chavs = GameObject.FindGameObjectWithTag("Chav");
        chavs.GetComponent<Chavs>().Damage(hitDamage);
        
    }
}

