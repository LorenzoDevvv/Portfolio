using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private WaveSpawner waveSpawner;
    public UnitHealth enemyHealth;

    [SerializeField] private int enemyMaxHealth = 200;
    [SerializeField] private int enemyCurrentHealth = 100;
    [SerializeField] private DamageFlash flashMaterial;

    ExplosionDamage explosionDamage;

    //[SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem explosionParticlePrefab;
    SoundPlayer soundPlayer;

    private void Start()
    {
        soundPlayer = GetComponent<SoundPlayer>();
        enemyHealth = new UnitHealth(enemyCurrentHealth, enemyMaxHealth);
        explosionDamage = GetComponent<ExplosionDamage>();
    }

    public void SetWaveSpawner(WaveSpawner spawner)
    {
        waveSpawner = spawner;
    }

    public void ResetHealth()
    {
        enemyHealth.Health = enemyHealth.MaxHealth;
    }
    public void EnemyTakeDmg(int dmg, GameObject attackingObject)
    {

        // Only apply damage if the attacking object is not the same as this object
        if (attackingObject != gameObject)
        {
            soundPlayer.PlaySound();
            if (flashMaterial != null)
            {
                flashMaterial.Flash();

            }

            enemyHealth.DmgUnit(dmg);
            if (enemyHealth.Health <= 0)
            {
                Destroy(gameObject);
                if (waveSpawner != null)
                {
                    Debug.Log("Enemy killed");
                    waveSpawner.EnemyKilled(gameObject);
                }
                if (explosionDamage != null)
                {
                    explosionDamage.Explode();
                }
                if (explosionParticlePrefab != null)
                {
                    ParticleSystem explosionInstance = Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);
                    //explosionInstance.Play();
                }
                //ResetHealth();
            }
        }
    }
}
