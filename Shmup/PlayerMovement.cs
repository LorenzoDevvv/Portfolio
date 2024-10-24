using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] public int health = 3;
    [SerializeField] private float speed = 10f;
    [SerializeField] float timerReset;

    public float m_timer = 10f;
    public float m_timer1 = 5f;

    bool qPressed = false;

    public bool shootSpeed = false;
    void Start()
    {
        
    }


    void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        transform.Translate(direction * Time.deltaTime * movementSpeed, 0, 0);

        float direction1 = Input.GetAxisRaw("Vertical");
        transform.Translate(0, direction1 * Time.deltaTime * movementSpeed, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (m_timer > 0)
        {
            m_timer = m_timer - 1 * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q) && m_timer <= 0)
        {
            qPressed = true;
            shootSpeed = true;
                 
        }

        if (m_timer1 > 0 && qPressed)
        {
            m_timer1 = m_timer1 - 1 * Time.deltaTime;
        }


        if (m_timer1 < 0)
        {
            qPressed = false;
            shootSpeed = false;
            m_timer = timerReset;
            m_timer1 = 5f;
        }


    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        newBullet.layer = gameObject.layer;
    }


    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("bhit");
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
