using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CitizenWeaponType
{
    Single = 0,
    Grenade = 1,
};

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon", order = 1)]
public class CitizenWeapon : ScriptableObject
{
    public string Name;

    public int Damage;

    public int Cost;

    public float FireRate;

    public float Range;

    public CitizenWeaponType Type;

    public GameObject Visual;
}
