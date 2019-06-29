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
            cooldown -= Time.deltaTime;
            return;
        }
        Alien enemy = EnemyInRange();
        if (enemy == null)
        {
            return;
        }
        FireWeapon(enemy);
    }

    private void FireWeapon(Alien enemy)
    {
        enemy.Health -= CitizenRole.GetDamage(EquipedWeapon);
        cooldown = CitizenRole.GetFireRate(EquipedWeapon);
    }

    private bool CanFireWeapon()
    {
        if (cooldown <= 0)
        {
            return true;
        }
        return false;
    }

    private Alien EnemyInRange()
    {
        for (int i = 0; i < LevelManager.Instance.Aliens.Count; i++)
        {
            Transform enemyTransform = LevelManager.Instance.Aliens[i].transform;
            if (Mathf.Pow(enemyTransform.position.x - transform.position.x, 2)
                + Mathf.Pow(enemyTransform.position.z - transform.position.z, 2)
                    < Mathf.Pow(CitizenRole.GetRange(EquipedWeapon), 2))
            {
                return LevelManager.Instance.Aliens[i];
            }
        }
        return null;
    }

    private IEnumerator MoveToLocation(Vector3 newLocation)
    {
        isMoving = true;

        // Determine how long it takes to finish the transition
        Vector3 tmpVector = newLocation - transform.position;
        tmpVector.y = 0;
        float magnitude2d = tmpVector.magnitude;
        float duration = magnitude2d / CitizenRole.MovementSpeed;

        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            transform.position = Vector3.MoveTowards(transform.position, newLocation, CitizenRole.MovementSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }

}
