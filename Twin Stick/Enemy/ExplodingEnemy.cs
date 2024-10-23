using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : MonoBehaviour
{
    private ExplosionDamage explosionDamage;
    public string targetTag = "Player";
    public float delay;
    EnemyBehaviour enemyBehaviour;
    private float radius;
    void Start()
    {
        explosionDamage = GetComponent<ExplosionDamage>();
        enemyBehaviour = FindObjectOfType<EnemyBehaviour>();
        radius = explosionDamage.radius;
        explosionDamage.shouldExplode = false;
    }

    private void Update()
    {
       

        //explosionDamage.hasExploded = false;
        // Check for colliders within the sphere
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // Filter colliders based on the target tag
        Collider targetCollider = System.Array.Find(colliders, collider => collider.CompareTag(targetTag));

        // Perform action if a target collider is found
        if (targetCollider != null)
        {

            explosionDamage.shouldExplode = true;

         
            Debug.Log("Player is inside the sphere!");

            
        }
    }

    
}
