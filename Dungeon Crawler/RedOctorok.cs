using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RedOctorok : Enemy
{
    [SerializeField] private int maxDistance = 4;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float chanceToShoot;
    protected override void Start()
    {
        base.Start();
        SetNextDestination();
    }

    protected override void Attack()
    {
        base.Attack();

        direction = CalculateDirection(rb.position, player.position);

        Invoke("ShootProjectile", Time.deltaTime * 15);

        Invoke("SetNextDestination", Time.deltaTime * 40);
    }

    protected float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }
    private void SetNextDestination()
    {
        direction = (LookDirection)Random.Range(0, 4);
        Vector2 directionVector = DirectionToVector2(direction);

        int randomDistance = Random.Range(1, maxDistance + 1);

        Vector2 destination = rb.position + directionVector * randomDistance;

        RaycastHit2D hit = Physics2D.Raycast(rb.position, directionVector, randomDistance, layerMask);

        if(hit.collider != null)
        {
            float distanceToHit = Vector2.Distance(rb.position, hit.point);

            if(distanceToHit >= 1)
            {
                float newDistance = distanceToHit - distanceToHit % 1;
                destination = rb.position + directionVector * newDistance;
            }
            else
            {
                destination = rb.position;
            }
        }

        if (!IsInsideViewport(destination))
        {
            destination = rb.position;  
        }

        SetDestination(destination);
    }

    protected LookDirection CalculateDirection(Vector2 pos1, Vector2 pos2, int amountdirections = 4)
    {
        float angle = 360f - CalculateAngle(pos1, pos2);

        float part = 360f / amountdirections;

        if (angle.Equals(360))
        {
            angle = 0;
        }

        int result = Mathf.RoundToInt(angle / part);

        if(result == amountdirections)
        {
            result = 0;
        }

        LookDirection direction = LookDirection.up;

        switch (result)
        {
            case 1:
                direction = LookDirection.right;
                break;
            case 2:
                direction = LookDirection.down;
                break;
            case 3:
                direction = LookDirection.left;
                break;
        }
        return direction;
    }

    protected Vector2 DirectionToVector2(LookDirection direction)
    {
        Vector2 directionVector = Vector2.zero;
        switch (direction)
        {
            case LookDirection.up:
                directionVector = Vector2.up;
                break;

            case LookDirection.left:
                directionVector = Vector2.left;
                break;

            case LookDirection.right:
                directionVector = Vector2.right;
                break;

            case LookDirection.down:
                directionVector = Vector2.down;
                break;
        }
        return directionVector;
    }
    protected override void ReachedDestination()
    {
        base.ReachedDestination();
        
        if(Random.value < chanceToShoot)
        {
            Attack();
        }
        else
        {
            Invoke("SetNextDestination", timerPerTile);
        }
    }

    public void ShootProjectile()
    {
        if (projectile == null)
        {
            Debug.Log("No projectile set on" + name);
            return;
        }

        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);

        Projectile p = proj.GetComponent<Projectile>();

        if(p == null)
        {
            Debug.LogError("Projectile not found on institated instance" + name);
            return;
        }
        p.Launch(gameObject, direction);
    }
    void Update()
    {
        
    }
}
