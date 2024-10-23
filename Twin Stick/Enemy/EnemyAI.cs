using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrol,
    Chase,
    Attack
}
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] waypoints;
    private int waypointIndex;
    private Vector3 target;
    public Transform player;
    public EnemyState currentState = EnemyState.Patrol;

    public Vector3 playerLastKnownPosition { get; set; }
    EnemyShootController enemyShootController;

    private const float outOfRangeCooldown = 3f;
    private float timeSinceLastSeen = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyShootController = GetComponent<EnemyShootController>();
        UpdateDestination();
        waypointIndex = 0;
        currentState = EnemyState.Patrol;
    }

    void Update()
    {
        //Make the enemy patrol
        if (currentState == EnemyState.Patrol)
        {
            if (Vector3.Distance(transform.position, target) < 1)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
        }
       
        if (currentState == EnemyState.Attack)
        {
            Attack();
        }

        if (currentState == EnemyState.Chase)
        {
            ChaseTarget();
        }
    }
    public void UpdateDestination()
    {
        //set destination to target position
        target = waypoints[waypointIndex].position;
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(target);
        }
    }
    
    void IterateWaypointIndex()
    {
        waypointIndex++;
        //Debug.Log(waypointIndex); 
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
         
    }
    public void StartChase()
    {
        //currentState = EnemyState.Chase;
       
        //agent.SetDestination(player.position);

    }

    public void ChaseTarget()
    {
        currentState = EnemyState.Chase;
        agent.SetDestination(player.position);
        timeSinceLastSeen += Time.deltaTime;
        Debug.Log(timeSinceLastSeen);
        if (timeSinceLastSeen >= outOfRangeCooldown)
        {
            Debug.Log("Lost sight of player");
            currentState = EnemyState.Patrol;
            UpdateDestination();
            timeSinceLastSeen = 0f;
        }

    }

    public void Attack()
    {
        agent.SetDestination(transform.position);
        enemyShootController.Shoot();
    }
    
}
