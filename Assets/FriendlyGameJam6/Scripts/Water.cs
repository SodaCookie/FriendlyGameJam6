using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Transform xf = GetComponent<Transform>();
        Vector3 euler = xf.eulerAngles;
        euler.y = Time.time;
        xf.eulerAngles = euler;
    }
}
