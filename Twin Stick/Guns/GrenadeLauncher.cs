using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    public Transform firePoint;
    public GameObject grenadePrefab;
    public float launchVelocity = 10f;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject p = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
            p.GetComponent<Rigidbody>().velocity = firePoint.up * launchVelocity;
        }
    }
}
