using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 2f; // Cooldown duration between attacks
    private float nextAttackTime; // Time when the enemy can attack again
    private PlayerBehaviour playerBehaviour;
    private PlayerArmor playerArmor;

    void Start()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        playerArmor = FindObjectOfType<PlayerArmor>();
        nextAttackTime = Time.time; // Initialize the next attack time
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        if (playerArmor.bulletProofVestIsOn)
        {
            playerArmor.armorPoints -= 1;
            playerArmor.armorSlider.value = playerArmor.armorPoints;
            Debug.Log(playerArmor.armorPoints);
        }
        else
        {
            playerBehaviour.PlayerTakeDmg(damage);
            Debug.Log(GameManager.gameManager.playerHealth.Health);
            Debug.Log("Player hit");
        }

        nextAttackTime = Time.time + attackCooldown; // Set the next attack time based on the cooldown
    }
}
