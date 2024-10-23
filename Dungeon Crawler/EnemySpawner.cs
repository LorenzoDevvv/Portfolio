using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyType[] enemies;
    [SerializeField] private float interval = 1;

    private float spawnTimer;
    private List<GameObject> spawnedEnemies;
    public int totalEnemies = 10;

    private SpriteRenderer spriteRenderer;
    private Color color;

    private void Awake()
    {
        spawnedEnemies = new List<GameObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        enabled = false;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (spawnedEnemies.Count == totalEnemies)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= interval)
        {
            SpawnNextEnemy();
            spawnTimer = 0f;
        }

    }

    private void SpawnNextEnemy()
    {
        string enemyPathName = "";

        switch (enemies[spawnedEnemies.Count])
        {
            case EnemyType.RedOctorok:
                enemyPathName = "Prefabs/Enemies/Red Octorok";
                break;
        }
        GameObject go = Instantiate(Resources.Load<GameObject>(enemyPathName), transform.position, Quaternion.identity);
        spawnedEnemies.Add(go);
        Debug.Log("Enemy spawned");
    }

    private void OnDisable()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            Destroy(spawnedEnemies[i]);
            spawnTimer = 0;
            spriteRenderer.color = Color.red;
        }
        spawnedEnemies.Clear();
    }

    private void OnEnable()
    {
        spriteRenderer.color = Color.white;
    }
}
