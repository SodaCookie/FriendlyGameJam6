using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Single = 0,
    Grenade = 1,
};

public class Weapon : MonoBehaviour
{
    public int Damage;

    public float FireRate;

    public float Range;

    public WeaponType Type;
}
