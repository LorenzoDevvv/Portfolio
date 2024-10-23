using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public float delay;
    float countdown;
    public bool hasExploded = false;
    public float force = 7;
    public GameObject explosionEffect;
    EnemyBehaviour enemyBehaviour;
    PlayerBehaviour playerBehaviour;
    public float radius;

    ExplodingEnemy explodingEnemy;
    //make a array f
    public bool shouldExplode = true;


    private void Start()
    {
        explodingEnemy = GetComponent<ExplodingEnemy>();
        countdown = delay;
        
    }

    private void Update()
    {
        if (shouldExplode)
        {
            countdown -= Time.deltaTime;
            //Debug.Log("Countdown: " + countdown);
            if (countdown <= 0f && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
    }
    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider c in colliders)
        {
            if (c.GetComponent<EnemyBehaviour>())
            {
                //get the enemybehaviour component
                enemyBehaviour = c.GetComponent<EnemyBehaviour>();
                //call the enemytakedmg method on it
                enemyBehaviour.EnemyTakeDmg(50, gameObject);
            }
            //if the player is in range - 50 
            if (c.GetComponent<PlayerBehaviour>())
            {
                playerBehaviour = c.GetComponent<PlayerBehaviour>();
                playerBehaviour.PlayerTakeDmg(50);
              
            }
        }

        Destroy(gameObject);
    }
    //draw gizmos of the overlap sphere
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy"))
        {
            Explode();
        }

        if (collision.collider.CompareTag("Surface"))
        {
            shouldExplode = true;
        }
    }

}
