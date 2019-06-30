using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Citizen", menuName = "Citizens/Nun Citizen", order = 1)]
public class NunType : CitizenType
{
    public float DebuffDuration;

    public int ArmorRemoval;

    public override IEnumerator OnDamageDebuff(CitizenWeapon weapon, Alien alien)
    {
        alien.Armor -= ArmorRemoval;
        yield return new WaitForSeconds(DebuffDuration);
        alien.Armor += ArmorRemoval;
    }
}
