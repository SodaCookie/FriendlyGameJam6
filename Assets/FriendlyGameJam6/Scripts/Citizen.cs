using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    public CitizenType CitizenRole;

    public CitizenWeapon EquipedWeapon;

    public Transform AttachmentPoint;

    public GameObject RangeIndicator;

    private float cooldown = 0;

    private bool isMoving = true;

    private GameObject visualObject;

    private Location currentLocation;

    private void Start()
    {
        UpdateRangeIndicator();
    }

    public void UpdateLocation(Location newLocation)
    {

        StopAllCoroutines();
        if (currentLocation != null)
        {
            currentLocation.Occupied = false;
        }
        currentLocation = newLocation;
        newLocation.Occupied = true;
        StartCoroutine(MoveToLocation(newLocation.transform.position));
    }

    public void EquipWeapon(CitizenWeapon weapon)
    {
        LevelManager.Instance.Command.RemoveWeapon(weapon.Name);
        EquipedWeapon = weapon;

        // Create the gun prefab
        if (weapon != null)
        {
            if (visualObject != null)
            {
                Destroy(visualObject);
            }
            visualObject = Instantiate(weapon.Visual, AttachmentPoint);
        }

        UpdateRangeIndicator();
    }

    private void UpdateRangeIndicator()
    {
        RangeIndicator.transform.localScale = new Vector3(1, 0.01f, 1) * CitizenRole.GetRange(EquipedWeapon);
    }

    private void Update()
    {
        if (LevelManager.Instance.PlayerInput.SelectedObject != null)
        {
            RangeIndicator.SetActive(LevelManager.Instance.PlayerInput.SelectedObject.Equals(gameObject));
        }
        else
        {
            RangeIndicator.SetActive(false);
        }

        if (!isMoving)
        {
            UpdateCombat();
        }
    }

    private void UpdateCombat()
    {
        Alien enemy = EnemyInRange();
        if (enemy != null)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        if (!CanFireWeapon())
        {
            cooldown -= Time.deltaTime;
            return;
        }

        if (enemy == null)
        {
            return;
        }

        FireWeapon(enemy);
    }

    private void FireWeapon(Alien enemy)
    {
        enemy.Health -= Mathf.Clamp(CitizenRole.GetDamage(EquipedWeapon) - enemy.Armor, 1, int.MaxValue);
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
            Vector3 oldPosition = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, newLocation, CitizenRole.MovementSpeed * Time.deltaTime);
            Vector3 direction = transform.position - oldPosition;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            yield return null;
        }
        isMoving = false;
        transform.position = newLocation;
    }
}
