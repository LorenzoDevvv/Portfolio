using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    [SerializeField] private float bulletSpeed = 0;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float minAttackTime = 1;
    [SerializeField] private float maxAttackTime = 5;
    private float attackTimer;
    private float attackInterval;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private bool attack;
    void Start()
    {
        attackInterval = Random.Range(minAttackTime, maxAttackTime);
    }
    void Update()
    {
        if (!attack) return;
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackInterval)
            {
                Shoot();
                attackTimer = 0;
            }
        }
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        attackInterval = Random.Range(minAttackTime, maxAttackTime);
    }
}
