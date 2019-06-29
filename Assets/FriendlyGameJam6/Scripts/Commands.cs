using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour
{
    public void CreateCitizen(GameObject citizenPrefab)
    {
        LevelManager.Instance.Player.Citizens.Add(Instantiate(citizenPrefab, LevelManager.Instance.CitizenSpawnPoint.position, Quaternion.identity));
        Citizen info = citizenPrefab.GetComponent<Citizen>();
        LevelManager.Instance.Player.Money -= info.CitizenRole.Cost;
    }

    public void SellCitizen(GameObject citizen)
    {
        Citizen info = citizen.GetComponent<Citizen>();
        LevelManager.Instance.Player.Money += Mathf.FloorToInt(info.CitizenRole.Cost * LevelManager.Instance.Constants.CitizenSellRatio);

        if (info.EquipedWeapon != null)
        {
            AddWeapon(info.EquipedWeapon);
        }

        LevelManager.Instance.Player.Citizens.Remove(citizen);
    }

    public void AddWeapon(CitizenWeapon weapon)
    {
        LevelManager.Instance.Player.Weapons.Add(weapon);
    }

    public CitizenWeapon RemoveWeapon(string name)
    {
        CitizenWeapon weaponToRemove = null;
        for (int i = 0; i < LevelManager.Instance.Player.Weapons.Count; i++)
        {
            if (LevelManager.Instance.Player.Weapons[i].Name == name)
            {
                weaponToRemove = LevelManager.Instance.Player.Weapons[i];
            }
        }
        if (weaponToRemove != null)
        {
            LevelManager.Instance.Player.Weapons.Remove(weaponToRemove);
            return weaponToRemove;
        }
        return null;
    }

    public void SellWeapon(string name)
    {
        LevelManager.Instance.Player.Money += RemoveWeapon(name).Cost;
    }

    public void BeginSpawning(Wave wave)
    {
        StartCoroutine(SpawningWaveCoroutine(wave));
    }

    private IEnumerator SpawningWaveCoroutine(Wave wave)
    {
        foreach (AlienGroup group in wave.Groups)
        {
            StartCoroutine(SpawningGroupCoroutine(group, wave.MinorDelay));
            yield return new WaitForSeconds(wave.MajorDelay);
        }
    }

    private IEnumerator SpawningGroupCoroutine(AlienGroup group, float minorDelay)
    {
        for (int i = 0; i < group.Amount; i++)
        {
            LevelManager.Instance.Aliens.Add(Instantiate(group.AlienType));
            yield return new WaitForSeconds(minorDelay);
        }
    }
}