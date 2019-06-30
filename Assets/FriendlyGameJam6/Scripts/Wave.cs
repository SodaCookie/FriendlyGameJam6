using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AlienGroup
{
    public int Amount;

    public Alien AlienType;

    public int SpawnPointIndex = 0;
}


[CreateAssetMenu(fileName = "New Wave", menuName = "Waves/New Wave", order = 1)]
public class Wave : ScriptableObject
{
    public float WaitForSeconds = float.PositiveInfinity;

    public List<AlienGroup> Groups;

    public CitizenWeapon Reward;

    [Tooltip("Delay spawn time between each monster in a single group")]
    public float MinorDelay;

    [Tooltip("Delay spawn time between each group")]
    public float MajorDelay;
}
