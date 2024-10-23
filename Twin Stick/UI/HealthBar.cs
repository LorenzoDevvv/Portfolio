using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider healthSlider;

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
    }
    public void setMaxHealth(int maxHealth)
    {
        //maximale health
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void setHealth(int health)
    {
        //health UI aanpassen
        healthSlider.value = health;
    }
}
