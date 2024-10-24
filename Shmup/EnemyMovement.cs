using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] int health = 3;

    public float moveSpeed = 5;
    void Start()
    {
        
    }
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= moveSpeed * Time.fixedDeltaTime;
        if(pos.x < -20)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
