using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Citizen", menuName = "Citizens/Basic Citizen", order = 1)]
public class CitizenType : ScriptableObject
{
    public float MovementSpeed;

    public int Cost;

    public string Name;

    public int DamageModifier;

    public int RangeModifier;

    [Tooltip("Percentage")]
    public float FireRateModifier;

    public virtual int GetDamage(Weapon weapon)
    {
        return weapon.Damage + DamageModifier;
    }

    public virtual float GetRange(Weapon weapon)
    {
        return weapon.Range + RangeModifier;
    }

    public virtual float GetFireRate(Weapon weapon)
    {
        return 1 / (weapon.FireRate * FireRateModifier);
    }
}
