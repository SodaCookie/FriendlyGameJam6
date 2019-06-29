using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    public int Damage = 1;

    public int Money;

    public int Health;

    public int Armor;

    public float MoveSpeed;

    public float Target;

    private void OnDestroy()
    {
        LevelManager.Instance.Player.Money += Money;
    }
}
