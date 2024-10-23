using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletTime : MonoBehaviour
{
    [SerializeField] private float timeToLive = 1f;
    ObjectPool objectPool;
    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();

        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeToLive);
        objectPool.ReturnObjectToPool("Shotgun Bullet", gameObject);

    }
}
