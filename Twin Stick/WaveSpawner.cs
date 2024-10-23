using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public List<GameObject> enemies;
        public List<Transform> spawnPoints;
    }

    public List<Wave> waves;
    public float timeBetweenWaves = 5f;

    private int currentWave = 0;
    private int totalEnemies;
    private int enemiesRemaining;
    private int enemiesSpawned; // New variable to track the number of enemies spawned
    private List<GameObject> spawnedEnemies;
    [SerializeField] Door door;
    public GameObject preSpawnIndicatorPrefab;

    private void Start()
    {
        // Disable the collider initially
        GetComponent<Collider>().enabled = false;

        // Calculate the total number of enemies across all waves
        CalculateTotalEnemies();
    }

    public GameObject spawnIndicatorPrefab;

    public void ActivateWaveSpawner()
    {
        // Start the first wave
        StartCoroutine(SpawnWave());

        // Disable the collider to prevent it from activating multiple times
        GetComponent<Collider>().enabled = false;
    }

    private IEnumerator SpawnWave()
    {
        Wave currentWaveData = waves[currentWave];
        enemiesRemaining = currentWaveData.enemies.Count;
        enemiesSpawned = 0; // Reset the enemiesSpawned count
        spawnedEnemies = new List<GameObject>();

        for (int i = 0; i < currentWaveData.enemies.Count; i++)
        {
            // Get the enemy from the current wave
            GameObject enemyPrefab = currentWaveData.enemies[i];

            // Get the spawn point for the current enemy
            Transform spawnPoint = currentWaveData.spawnPoints[i % currentWaveData.spawnPoints.Count];

            // Instantiate the pre-spawn indicator
            GameObject preSpawnIndicator = Instantiate(preSpawnIndicatorPrefab, spawnPoint.position, spawnPoint.rotation);
            preSpawnIndicator.SetActive(true);

            yield return new WaitForSeconds(1f); // Wait for a short delay before activating the spawn indicator

            // Instantiate the spawn indicator
            GameObject spawnIndicator = Instantiate(spawnIndicatorPrefab, spawnPoint.position, spawnPoint.rotation);
            spawnIndicator.SetActive(true);

            // Instantiate a new enemy
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Attach the EnemyBehaviour script to the enemy object if not already attached
            EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour == null)
            {
                enemyBehaviour = enemy.AddComponent<EnemyBehaviour>();
            }

            // Set the WaveSpawner reference in the EnemyBehaviour script
            enemyBehaviour.SetWaveSpawner(this);

            // Add the enemy to the spawnedEnemies list immediately after instantiation
            spawnedEnemies.Add(enemy);

            // Increment the enemiesSpawned count
            enemiesSpawned++;

            // Deactivate the pre-spawn indicator and spawn indicator
            Destroy(preSpawnIndicator);
            //Destroy(spawnIndicator);

            yield return new WaitForSeconds(1f); // Wait for a short delay between enemy spawns
        }
    }

    public void EnemyKilled(GameObject enemy)
    {
        Debug.Log("Enemy killed!");

        if (spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy);
        }
        else
        {
            Debug.LogWarning("Killed enemy not found in the spawnedEnemies list!");
        }

        enemiesRemaining--;

        if (enemiesRemaining <= 0 && enemiesSpawned >= totalEnemies)
        {
            if (currentWave < waves.Count - 1)
            {
                StartCoroutine(StartNextWave());
            }
            else
            {
                // All waves completed
                Debug.Log("All waves completed!");

                // Check if it is the last wave and the last enemy is killed
                if (currentWave == waves.Count - 1 && spawnedEnemies.Count == 0 && door != null)
                {
                    door.OpenDoor(); // Open the door
                    //SpawnKeycard(); // Spawn the keycard
                }
            }
        }
    }

    private IEnumerator StartNextWave()
    {
        currentWave++;
        enemiesRemaining = waves[currentWave].enemies.Count;
        enemiesSpawned = 0; // Reset the enemiesSpawned count
        spawnedEnemies.Clear(); // Clear the list of spawned enemies

        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave());
    }

    private void CalculateTotalEnemies()
    {
        totalEnemies = 0;

        foreach (Wave wave in waves)
        {
            totalEnemies += wave.enemies.Count;
        }
    }
}
