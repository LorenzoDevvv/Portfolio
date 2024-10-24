using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private float speed = 10f;
    [SerializeField] GameObject hitEffect;
    [SerializeField] private float minAttackTime = 1;
    [SerializeField] private float maxAttackTime = 5;
    private float attackTimer;
    private float attackInterval;
    [SerializeField] private Transform bulletSpawnPoint;

    float m_timer = 3f;
    float m_timer1 = 5f;

    public bool shootSpeed = false;
    private float chargeAttackSpeed = 30f;
    private float shootCount;
    public float startShootCount;

    public PlayerMovement movement;

    public void Awake()
    {
        movement = FindObjectOfType<PlayerMovement>();
    }
    private void Start()
    {
        Destroy(gameObject, 8f);
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);


        //SpecialAttack
        if (movement.shootSpeed == true)
        {
            speed = 20f;

            if (shootSpeed == true)
            {
                
            }
            m_timer = 3f;

        }
        else if(movement.shootSpeed == false)
        {
            speed = 10f;
        }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitEffect, transform.position, transform.rotation);   
        Destroy(gameObject);
    }
}
