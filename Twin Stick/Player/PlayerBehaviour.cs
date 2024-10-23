using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    PlayerDash playerDash;
    [SerializeField] public float cooldown = 2f;
    private float timeLeft;
    [SerializeField] DeathScreen deathScreen;
    [SerializeField] GameObject explosionPrefab;


    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerTakeDmg(200);
        }
        playerDash = GetComponent<PlayerDash>();

        //wanneer de health onder of gelijk aan 0 is gaat de speler dood
        if (GameManager.gameManager.playerHealth.Health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            cooldown -= Time.deltaTime;
            //if (deathScreen != null)
            //{
            //    deathScreen.IsActive = true;
            //}

            Invoke("PlayerDeath", cooldown);
            //deathScreen.enabled = true;
            //after a invoke the player will be able to move again
            //Wait();
            gameObject.SetActive(false);
        }

    }

    public void PlayerTakeDmg(int dmg)
    {
        if (!playerDash.isInvulnerable)
        {
            GameManager.gameManager.playerHealth.DmgUnit(dmg);
            if (healthBar != null)
            {
                healthBar.setHealth(GameManager.gameManager.playerHealth.Health);
            }
            Debug.Log(GameManager.gameManager.playerHealth.Health);
        }
      
    }

    public void PlayerHeal(int healing)
    {
        GameManager.gameManager.playerHealth.HealUnit(healing);
        healthBar.setHealth(GameManager.gameManager.playerHealth.Health);
    }

    public void PlayerDeath()
    {
        //healthBar.setHealth(GameManager.gameManager.playerHealth.Health);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (deathScreen != null)
        {
            deathScreen.IsActive = false;
        }

    }
}
