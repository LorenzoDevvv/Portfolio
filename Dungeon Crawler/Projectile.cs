using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float projectileSpeed;
    [SerializeField] private float timeTillDestroy = 5f;

    private LookDirection direction;
    private Vector3 directionVector;
    private float aliveTime;
    private float damageOutput = 0.25f;
   
    public void Launch(GameObject owner, LookDirection direction)
    {
        SetDirection(direction);
        Physics2D.IgnoreCollision(owner.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        GetComponent<Rigidbody2D>().AddForce(directionVector * projectileSpeed, ForceMode2D.Impulse);
    }

    private void SetDirection(LookDirection lookDirection)
    {
        direction = lookDirection;

        switch (direction)
        {
            case LookDirection.up:
                directionVector = Vector3.up;
                break;

            case LookDirection.right:
                directionVector = Vector3.right;
                transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                break;

            case LookDirection.down:
                directionVector = Vector3.down;
                transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                break;

            case LookDirection.left:
                directionVector = Vector3.left;
                transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damageOutput);
            //collision.gameObject.GetComponent<PlayerAnimator>().Hit();
        }

        Destroy(gameObject);

        //ChangeHealth(1f);
    }
    public void Update()
    {
        aliveTime += Time.deltaTime;
        if (aliveTime > timeTillDestroy)
        {
            Destroy(gameObject);
        }


    }
}
