using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Citizen", menuName = "Citizens/Scientist Citizen", order = 1)]
public class ScientistType : CitizenType
{
    public int TickAmount;

    public int NumberOfTicks;

    public float TickPeriod;

    public override IEnumerator OnDamageDebuff(CitizenWeapon weapon, Alien alien)
    {
        for (int i = 0; i < TickPeriod; i++)
        {
            alien.Health -= TickAmount;
            yield return new WaitForSeconds(TickPeriod);
        }
    }
}
