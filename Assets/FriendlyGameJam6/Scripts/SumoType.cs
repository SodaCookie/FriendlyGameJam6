using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Citizen", menuName = "Citizens/Sumo Citizen", order = 1)]
public class SumoType : CitizenType
{
    public override CitizenWeaponType GetWeaponType(CitizenWeapon weapon)
    {
        return CitizenWeaponType.Grenade;
    }
}
