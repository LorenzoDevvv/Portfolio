using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Vector2 velocity;
    private Rigidbody2D rb;
    public bool isPaused;

    [SerializeField] private float speed = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        
        float directionx = Input.GetAxisRaw("Horizontal") * speed;
        
        float directiony = Input.GetAxisRaw("Vertical") * speed;

        if (directiony != 0)
        {
            directionx = 0;
        }

        velocity = new Vector2(directionx, directiony);

    }

    private void FixedUpdate()
    {
        if (isPaused) return;                
       rb.velocity = velocity * speed;
    
    }

    public void SetPause(bool pause)
    {
        isPaused = pause;

        if (pause)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
