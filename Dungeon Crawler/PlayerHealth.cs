using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    PlayerAnimator playerAnim;

    public override void Start()
    {
        base.Start();
        playerAnim = GetComponent<PlayerAnimator>();
    }

    public override void ChangeHealth(float amount)
    {
        base.ChangeHealth(amount);
    }
    protected override void CheckHealth()
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }
        else
        {
            playerAnim.SetAnimPlayerState(PlayerState.Idle);
        }

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public override void Kill()
    {
        base.Kill();
        playerAnim.TriggerDeathAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAnim.Hit();
            ChangeHealth(-0.5f);
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
