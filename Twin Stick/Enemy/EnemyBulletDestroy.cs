using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDestroy : MonoBehaviour
{
    PlayerBehaviour playerBehaviour;
    [SerializeField] private float timeToLive = 5f;
    [SerializeField] HealthBar healthBar;
    PlayerArmor playerArmor;

    private void Start()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        playerArmor = FindObjectOfType<PlayerArmor>();
        StartCoroutine(DestroyBullet());
    }

    private void Update()
    {
     
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            if (playerArmor.bulletProofVestIsOn)
            {
                playerArmor.armorPoints -= 1;
                playerArmor.armorSlider.value = playerArmor.armorPoints;
                Debug.Log(playerArmor.armorPoints);

            }
            else
            {
                playerBehaviour.PlayerTakeDmg(10);
                Debug.Log(GameManager.gameManager.playerHealth.Health);
                //playerBehaviour.isHit = true;
                Debug.Log("Player hit");
                Destroy(gameObject);
            }
        }
        Destroy(gameObject);

    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
