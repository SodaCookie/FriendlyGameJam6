using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneOfProtection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if (other.attachedRigidbody != null)
        {
            Alien alien = other.attachedRigidbody.GetComponent<Alien>();
            if (alien)
            {
                LevelManager.Instance.Player.Lives -= alien.Damage;
                Destroy(alien);
            }
        }
    }
}
