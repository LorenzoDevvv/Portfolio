using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeToLive = 5f;
    [SerializeField] private ParticleSystem impactPrefab;

    List<EnemyAI> enemyAIs;
    
    
    EnemyBehaviour enemyBehaviour;

    Vector3 moveVector;
    ObjectPool objectPool;
    void Start()
    {
        moveVector = Vector3.forward * speed * Time.fixedDeltaTime;
        enemyBehaviour = FindObjectOfType<EnemyBehaviour>();
        objectPool = FindObjectOfType<ObjectPool>();
        enemyAIs = new List<EnemyAI>();
        EnemyAI[] enemyAIArray = FindObjectsOfType<EnemyAI>();
        foreach (EnemyAI enemyAI in enemyAIArray)
        {
            enemyAIs.Add(enemyAI);
        }
        //soundPlayer = GetComponent<SoundPlayer>();

     
        
    }

    private void Awake()
    {

    }
    private void Update()
    {
        StartCoroutine(DestroyBullet());
    }
    
    
    private void FixedUpdate()
    {
        transform.Translate(moveVector);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impactPrefab, transform.position, Quaternion.identity);
        // Check if the collision was with an object tagged as "Enemy"
        if (collision.collider.CompareTag("Enemy"))
        {
            // Call the StartChase method on the enemyAI component

            // Get a reference to the enemy object that was hit
            GameObject enemyObject = collision.gameObject;

            EnemyAI enemyAI = enemyObject.GetComponent<EnemyAI>();
            if (enemyAI != null && enemyAIs.Contains(enemyAI))
            {
                // Find the index of the enemyAI in the list
                int index = enemyAIs.IndexOf(enemyAI);

                // Call the StartChase method on the corresponding enemyAI component
                if (index < enemyAIs.Count)
                {
                    enemyAIs[index].ChaseTarget();
                }
            }
            
            // Get the EnemyBehaviour component from the enemy object
            // and call the EnemyTakeDmg method on it
            EnemyBehaviour enemyBehaviour = enemyObject.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.EnemyTakeDmg(12,gameObject);
            }
        }
        // Destroy the bullet object
        objectPool.ReturnObjectToPool("Bullet", gameObject);
        //gameObject.SetActive(false);
    }



    

    
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeToLive);
        //Debug.Log("Bullet Destroyed");
        objectPool.ReturnObjectToPool("Bullet", gameObject);
    }

}
