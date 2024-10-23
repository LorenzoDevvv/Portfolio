    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyShootController : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float nextFireTime;
    [SerializeField] private Transform spawnPoint;
    public bool shouldShoot = false;

    private void Update()
    {
        if (shouldShoot)
        {
            Shoot();
            
        }
      
    }
    public void Shoot()
    {
        // Calculate the direction towards the player
        Vector3 direction = player.position - transform.position;

        // Rotate towards the player
        transform.rotation = Quaternion.LookRotation(direction);

        // Check if enough time has passed since last fire
        if (Time.time >= nextFireTime)
        {
            // Set the next fire time
            nextFireTime = Time.time + 1f / fireRate;

            // Instantiate a bullet and set its direction
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = direction.normalized * 10f;
        }
    }
}
