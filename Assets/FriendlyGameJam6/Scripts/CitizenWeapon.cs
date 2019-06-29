using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CitizenWeaponType
{
    Single = 0,
    Grenade = 1,
};

public class CitizenWeapon : MonoBehaviour
{
    public int Damage;

    public int Cost;

    public float FireRate;

    public float Range;

    public CitizenWeaponType Type;
}
