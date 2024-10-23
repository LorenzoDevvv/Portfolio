using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{

    public float speed = 10f;
    public int damage = 10;
    public int maxPierces = 3;  // Maximum number of enemies the bullet can pierce through

    private int pierceCount = 0;  // Current number of enemies pierced
    ObjectPool objectPool;

    public GameObject impactPrefab;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        StartCoroutine(DestroyBullet());
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.gameObject.name);

        Instantiate(impactPrefab, transform.position, Quaternion.identity);

        EnemyBehaviour enemyHealth = other.GetComponent<EnemyBehaviour>();
        if (enemyHealth != null)
        {
            enemyHealth.EnemyTakeDmg(damage, gameObject);
            pierceCount++;

            if (pierceCount >= maxPierces)
            {
                gameObject.SetActive(false); // inactive the bullet if it has reached the maximum number of pierces
            }
        }

        if (other.gameObject.tag == "Enviroment")
        objectPool.ReturnObjectToPool("Sniper Bullet", gameObject);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2);
        //Debug.Log("Bullet Destroyed");
        objectPool.ReturnObjectToPool("Sniper Bullet", gameObject);
    }
}
