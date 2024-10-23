using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashDistance = 5f; 
    public float dashDuration = 0.2f; 
    public float dashCooldown = 1f; 
    public bool isInvulnerable = false; 

    private bool canDash = true; 
    private CharacterController characterController; 
    private Vector3 dashDirection; 
    private float dashTimer = 0f; 
    private float cooldownTimer = 0f; 


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the dash is on cooldown
        if (!canDash)
        {
            // Increment the cooldown timer
            cooldownTimer += Time.deltaTime;

            // Check if the cooldown duration has elapsed
            if (cooldownTimer >= dashCooldown)
            {
                // Cooldown has ended, player can dash again
                canDash = true;
                cooldownTimer = 0f;
            }
        }

        // Check for dash input
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Set the dash direction based on player input
            dashDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

            // Start the dash
            StartDash();
        }

        // Handle the dash
        if (dashTimer > 0f)
        {
            // Enable invulnerability while dashing
            isInvulnerable = true;

            // Move the player along the dash direction
            characterController.Move(dashDirection * dashDistance / dashDuration * Time.deltaTime);

            // Decrement the dash timer
            dashTimer -= Time.deltaTime;

            // Check if the dash duration has elapsed
            if (dashTimer <= 0f)
            {
                // Dash has ended
                dashTimer = 0f;
                canDash = false;

                // Disable invulnerability after dashing
                isInvulnerable = false;
            }
        }
    }
    void StartDash()
    {
        // Set the dash timer
        dashTimer = dashDuration;
    }
}

