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

    public int DefaultDamage = 1;

    public int DefaultRange = 1;

    [Tooltip("Percentage")]
    public float DefaultFirerate = 1;

    [Tooltip("Percentage")]
    public float FireRateModifier;

    public CitizenWeaponType DefaultWeaponType = CitizenWeaponType.Single;

    public GameObject PrefabGameObject;

    public virtual int GetDamage(CitizenWeapon weapon)
    {
        if (weapon == null)
        {
            return DefaultDamage;
        }
        return weapon.Damage + DamageModifier;
    }

    public virtual float GetRange(CitizenWeapon weapon)
    {
        if (weapon == null)
        {
            return DefaultRange;
        }
        return weapon.Range + RangeModifier;
    }

    public virtual CitizenWeaponType GetWeaponType(CitizenWeapon weapon)
    {
        if (weapon == null)
        {
            return DefaultWeaponType;
        }
        return weapon.Type;
    }

    public virtual float GetFireRate(CitizenWeapon weapon)
    {
        if (weapon == null)
        {
            return 1 / DefaultFirerate;
        }
        return 1 / (weapon.FireRate * FireRateModifier);
    }
}
