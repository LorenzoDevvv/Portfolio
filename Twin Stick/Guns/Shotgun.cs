using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : Gun
{
    //[SerializeField] private Transform gunPoint;
    public int numberOfProjectiles;
    public float projectileSpeed;
    public GameObject ProjectilePrefab;


    private Vector3 startPoint;
    private const float radius = 1f;


    private PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();

    }

    

    protected override void Shoot(int _numberOfProjectiles)
    {
        if (shootParticle != null && smokeParticle != null && trailParticle != null)
        {
            shootParticle.Emit(5);
            smokeParticle.Emit(5);
            trailParticle.Emit(15);
        }

        soundPlayer.PlaySound();

        currentAmmo -= numberOfProjectiles;
        maxAmmo -= numberOfProjectiles;


        Vector3 startPoint = gunPoint.position; // Calculate the startPoint outside of the Shoot method
        float angleStep = 90f / _numberOfProjectiles;
        float angle = transform.eulerAngles.y - 38;

        for (int i = 0; i <= _numberOfProjectiles - 1; i++)
        {
            // Direction calculations
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - startPoint) * projectileSpeed;

            GameObject bullet = objectPool.GetPooledObject(bulletType);
            if (bullet != null)
            {
                bullet.transform.position = startPoint;
                bullet.transform.rotation = Quaternion.LookRotation(projectileMoveDirection);
                bullet.SetActive(true);

                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                if (bulletRigidbody != null)
                {
                    bulletRigidbody.velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.y);
                }
            }

            angle += angleStep;
        }

        canShoot = false;
        StartCoroutine(ShotCooldown());
        InvokeBulletShotEvent(currentAmmo);
    }


    void Start()
    {
        
    }

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            // Handle common gun functionality
            if (maxAmmo == 0)
                return;

            if (isReloading)
                return;
            GetPlayerInput();

            if (currentAmmo <= 0)
            {
                Reload();
            }
        }
    }

    protected override void GetPlayerInput()
    {
        if (Input.GetButton("Shoot") && canShoot)
        {
            Debug.Log("working");
            Shoot(numberOfProjectiles);
           
        }

    }
    private void SpawnProjectile(int _numberOfProjectiles)
    {
     
    }
}
