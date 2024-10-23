using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttack : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float nextFireTime;
    [SerializeField] private Transform spawnPoint;
    public bool isShooting;
    public bool isWalking;

    public float walkTime = 3f;
    private float walkCounter;

    public int canShootAmount;

    FieldOfView fieldOfView;
    NavMeshAgent agent;
    
    public int resetAmount;

    public float minWalkTime = 1f;
    public float maxWalkTime = 5f;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fieldOfView = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        StartWalking();
        canShootAmount = resetAmount;

        //if (Random.value < 0.5f)
        //{
        //    StartWalking();
        //}
        //else
        //{
        //    StartShooting();
        //}
    }

    void Update()
    {
        if (isWalking)
        {
            Walk();
        }
        else if (isShooting)
        {
            Shoot();
        }

        else if (fieldOfView.visibleTargets.Contains(fieldOfView.playerTransform))
        {
            Debug.Log("Player in view");
            StartShooting();
        }
        else
        {
            Debug.Log("Player not in view");
            StopShooting();
            StartWalking();
        }
    }



    void Walk()
    {
        agent.SetDestination(player.position);


        // Generate a random timeout between minWalkTime and maxWalkTime
        float randomTimeout = Random.Range(minWalkTime, maxWalkTime);

        // Reset the walkCounter with the random timeout
        walkTime = randomTimeout;
        
        walkCounter += Time.deltaTime;

        if (walkCounter >= walkTime)
        {
            // Stop walking and start shooting after the walk interval
            StopWalking();
            StartShooting();

        }
    }


    void Shoot()
    {
        
            // Calculate the direction towards the player
            Vector3 direction = player.position - transform.position;

            // Rotate towards the player
            transform.rotation = Quaternion.LookRotation(direction);

            // Check if enough time has passed since last fire
            if (Time.time >= nextFireTime && canShootAmount > 0)
            {
                // Set the next fire time
                nextFireTime = Time.time + 1f / fireRate;

                // Instantiate a bullet and set its direction
                GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = direction.normalized * 10f;
                canShootAmount--;

                if (canShootAmount <= 0)
                {
                    // Stop shooting and start walking after firing the last bullet
                    StopShooting();
                    StartWalking();
                }
            }
     
    }


    void StartShooting()
    {
        isShooting = true;
        isWalking = false;
        canShootAmount = resetAmount; // Reset the number of shots
    }

    void StopShooting()
    {
        isShooting = false;
    }

    void StartWalking()
    {
        isWalking = true;
        walkCounter = 0;
        agent.isStopped = false;
    }

    void StopWalking()
    {
        isWalking = false;
        agent.isStopped = true;
        agent.SetDestination(transform.position);
    }
}
