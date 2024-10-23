using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletDamage : MonoBehaviour
{

    ObjectPool objectPool;
    public GameObject impactPrefab;

    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impactPrefab, transform.position, Quaternion.identity);

        // Check if the collision was with an object tagged as "Enemy"
        if (collision.collider.CompareTag("Enemy"))
        {
            // Get a reference to the enemy object that was hit
            GameObject enemyObject = collision.gameObject;

            // Get the EnemyBehaviour component from the enemy object

            // and call the EnemyTakeDmg method on it
            EnemyBehaviour enemyHealth = enemyObject.GetComponent<EnemyBehaviour>();
            if (enemyHealth != null)
            {
                enemyHealth.EnemyTakeDmg(20, gameObject);
            }
        }
        // Destroy the bullet object
        objectPool.ReturnObjectToPool("Shotgun Bullet", gameObject);
    }
}
