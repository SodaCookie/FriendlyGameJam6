using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity = 50f;

    public GameObject CreateOnArrival;

    public void Fire(Vector3 target)
    {
        StartCoroutine(TravelToLocation(target));
    }

    private IEnumerator TravelToLocation(Vector3 target)
    {
        while (Vector3.Distance(target, transform.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, velocity * Time.deltaTime);
            yield return null;
        }
        if (CreateOnArrival != null)
        {
            Instantiate(CreateOnArrival, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
