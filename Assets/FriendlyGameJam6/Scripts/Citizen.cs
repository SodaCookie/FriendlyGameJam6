using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    public CitizenType CitizenRole;

    public CitizenWeapon EquipedWeapon;

    private float cooldown = 0;
    private bool isMoving = false;

    public void UpdateLocation(Vector3 newLocation)
    {
        StartCoroutine(MoveToLocation(newLocation));
    }

    private void Update()
    {
        if (!isMoving)
        {
            UpdateCombat();
        }
    }

    private void UpdateCombat()
    {
        if (!CanFireWeapon())
        {
            return;
        }
        if (!IsEnemyInRange())
        {
            return;
        }
        FireWeapon();
    }

    private void FireWeapon()
    {
        throw new NotImplementedException();
    }

    private bool CanFireWeapon()
    {
        if (cooldown <= 0)
        {
            cooldown = CitizenRole.GetFireRate(EquipedWeapon);
            return true;
        }
        cooldown -= Time.deltaTime;
        return false;
    }

    private bool IsEnemyInRange()
    {
        throw new NotImplementedException();
    }

    private IEnumerator MoveToLocation(Vector3 newLocation)
    {
        isMoving = true;
        yield return null;
        isMoving = false;
    }

}
