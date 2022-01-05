using UnityEngine;
using UnityEngine.EventSystems;

public class ChavBodyHitbox : Toucher
{
    [SerializeField] int hitDamage = 20;
    GameObject chavs;

    private void Start()
    {
        chavs = GameObject.FindGameObjectWithTag("Chav");
    }

    protected override void ActionOnTouch(PointerEventData eventData)
    {
        chavs.GetComponent<Chavs>().Damage(hitDamage);
        chavs.GetComponent<Chavs>().DrawInjuries(LastTouchPoint);
    }
}

