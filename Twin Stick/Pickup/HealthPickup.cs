using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthPickup : MonoBehaviour, ICollectible
{
    public int healthRestored = 10;
    PlayerBehaviour playerBehaviour;
    public static event Action OnHealthPickupCollected;

    private void Start()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }
    public void Collect()
    {
        if (OnHealthPickupCollected != null)
        {
            OnHealthPickupCollected();
        }
        playerBehaviour.PlayerHeal(healthRestored);        
        Debug.Log(GameManager.gameManager.playerHealth.Health);
        Destroy(gameObject);

    }
}
