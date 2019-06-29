using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AlienGroup
{
    public int Amount;

    public Alien AlienType;
}


[CreateAssetMenu(fileName = "New Wave", menuName = "Waves/New Wave", order = 1)]
public class Wave : ScriptableObject
{
    public List<AlienGroup> Groups;
}
