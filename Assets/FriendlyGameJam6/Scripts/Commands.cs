using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour
{
    public void CreateCitizen()
    {
        throw new NotImplementedException();
    }

    public void AddWeapon()
    {
        throw new NotImplementedException();
    }

    public void BeginSpawning(Wave wave)
    {
        StartCoroutine(SpawningCoroutine(wave));
    }

    private IEnumerator SpawningCoroutine(Wave wave)
    {
        throw new NotImplementedException();
    }
}