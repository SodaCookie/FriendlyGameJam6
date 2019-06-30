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

    public List<AudioClip> OnSpawnAudio = new List<AudioClip>();

    public List<AudioClip> OnMoveAudio = new List<AudioClip>();

    public List<AudioClip> OnEquipAudio = new List<AudioClip>();

    private float cooldown = 0;

    private bool isMoving = true;

    private GameObject visualObject;

    private Location currentLocation;

    private void Start()
    {
        UpdateRangeIndicator();

        if (OnSpawnAudio.Count > 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(OnSpawnAudio[UnityEngine.Random.Range(0, OnSpawnAudio.Count)]);
        }
    }

    public void ClearLocation()
    {
        if (currentLocation != null)
        {
            currentLocation.Occupied = false;
        }
    }

    public void UpdateLocation(Location newLocation)
    {

        StopAllCoroutines();
        ClearLocation();
        currentLocation = newLocation;
        newLocation.Occupied = true;
        StartCoroutine(MoveToLocation(newLocation.transform.position));
    }

    public void EquipWeapon(CitizenWeapon weapon)
    {
        if (LevelManager.Instance.Constants.RemoveOnEquip)
        {
            LevelManager.Instance.Command.RemoveWeapon(weapon.Name);
        }
        else
        {
            LevelManager.Instance.Command.RemoveWeapon(weapon.Name);
            if (EquipedWeapon != null)
            {
                LevelManager.Instance.Command.AddWeapon(EquipedWeapon);
            }
        }
        EquipedWeapon = weapon;
        if (OnEquipAudio.Count > 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(OnEquipAudio[UnityEngine.Random.Range(0, OnEquipAudio.Count)]);
        }


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
        RangeIndicator.transform.localScale = new Vector3(1, 0.001f, 1) * CitizenRole.GetRange(EquipedWeapon);
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
        if (EquipedWeapon != null)
        {
            GetComponent<AudioSource>().PlayOneShot(EquipedWeapon.Sound);
            if (EquipedWeapon.Bullet != null)
            {
                GameObject bullet = Instantiate(EquipedWeapon.Bullet, AttachmentPoint.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().Fire(enemy.transform.position + Vector3.up * 2f + UnityEngine.Random.insideUnitSphere * 0.4f);
            }
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(LevelManager.Instance.Constants.PunchSound);
        }
        if (CitizenRole.GetWeaponType(EquipedWeapon) == CitizenWeaponType.Single)
        {

            DamageEnemy(enemy);
        }
        else if (CitizenRole.GetWeaponType(EquipedWeapon) == CitizenWeaponType.Grenade)
        {
            Vector2 impactPoint = new Vector2(enemy.transform.position.x, enemy.transform.position.z);
            // Get all enemies in the area of damage
            for (int i = 0; i < LevelManager.Instance.Aliens.Count; i++)
            {
                Vector2 enemyPosition = new Vector2(LevelManager.Instance.Aliens[i].transform.position.x, LevelManager.Instance.Aliens[i].transform.position.z);
                if ((enemyPosition - impactPoint).sqrMagnitude < Mathf.Pow(LevelManager.Instance.Constants.GrenadeRadius, 2))
                {
                    DamageEnemy(LevelManager.Instance.Aliens[i]);
                }
            }
        }

        cooldown = CitizenRole.GetFireRate(EquipedWeapon);
    }

    private void DamageEnemy(Alien enemy)
    {
        enemy.Health -= Mathf.Clamp(CitizenRole.GetDamage(EquipedWeapon) - enemy.Armor, 1, int.MaxValue);
        IEnumerator damageCoroutine = CitizenRole.OnDamageDebuff(EquipedWeapon, enemy);
        if (damageCoroutine != null)
        {
            enemy.StartCoroutine(damageCoroutine);
        }
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

        if (OnMoveAudio.Count > 0)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(OnMoveAudio[UnityEngine.Random.Range(0, OnMoveAudio.Count)]);
        }

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
