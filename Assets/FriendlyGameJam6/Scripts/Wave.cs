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

public class Wave : MonoBehaviour
{
    public List<AlienGroup> Groups;

    public void BeginSpawning()
    {
        StartCoroutine(SpawningCoroutine());
    }

    private IEnumerator SpawningCoroutine()
    {
        throw new NotImplementedException();
    }
}
