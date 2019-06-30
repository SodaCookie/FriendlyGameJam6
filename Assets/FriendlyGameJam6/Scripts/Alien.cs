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
       
    void Start () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = LevelManager.Instance.EnemyDestination.position;
        agent.speed = MoveSpeed;
    }

    void Update() {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Walk", agent.velocity.magnitude > 0.01 && agent.velocity.magnitude < 2);
        animator.SetBool("SprintSlide", agent.velocity.magnitude > 2);

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Health <= 0)
        {
            LevelManager.Instance.Player.Money += Money;
        }
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.Aliens.Remove(this);
        }
    }
}
