using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth
{
    //Fields
    int currentHealth;
    int currentMaxHealth;

    //Properties
    public int Health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value; 
        }

    } 
    public int MaxHealth
    {
        get
        {
            return currentMaxHealth;
        }
        set
        {
            currentMaxHealth = value; 
        }
    }
    // Constructor 
    public UnitHealth(int health, int maxHealth)
    {
        currentHealth = health;
        currentMaxHealth = maxHealth;
    }

    //methods
    public void DmgUnit(int dmgAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= dmgAmount;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }
    }
    public void HealUnit(int healAmount)
    {
        if (currentHealth < currentMaxHealth)
        {
            currentHealth += healAmount;
        }
        if (currentHealth > currentMaxHealth)
        {
            currentHealth = currentMaxHealth;
        }
    }
}
