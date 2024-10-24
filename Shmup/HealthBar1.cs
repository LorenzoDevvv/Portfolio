using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar1 : MonoBehaviour
{
    private Image Healthbar;
    public float CurrentHealth;
    private float MaxHealth = 50f;
    EnemyMovement Boss;
  

    private void Start()
    {
        Healthbar = GetComponent<Image>();
        Boss = FindObjectOfType<EnemyMovement>();
    }

    private void Update()
    {
        //CurrentHealth = Boss.Health;
        //HealthBar1.fillAmount = CurrentHealth / MaxHealth;
    }
}
