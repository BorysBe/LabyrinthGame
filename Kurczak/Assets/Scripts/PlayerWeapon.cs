using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int damage = 20;

    public int DamageDealed()
    {
        return damage;
    }
}