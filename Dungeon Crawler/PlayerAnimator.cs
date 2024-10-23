using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LookDirection
{
    up = 0,
    left,
    right,
    down
}

public enum PlayerState
{
   Idle = 0,
   walking,
   attacking,
   dead
}
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private LookDirection lookDirection;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Animator animator;

    [Header("Input")]
    private bool Fire1;
    private bool Fire2;
    [SerializeField] private float xAxis;
    [SerializeField] private float yAxis;
    [SerializeField] private Color[] flashColors;

    private SpriteRenderer playerRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetAnimPlayerState(PlayerState enumChange)
    {
        playerState = enumChange;
        animator.SetInteger("PlayerState", (int)playerState);
        Debug.Log("Test");
    }

    void Update()
    {
        CheckMovement();
    }

    private void CheckMovement()
    {
        if (playerState != PlayerState.dead)
        {
            xAxis = Input.GetAxis("Horizontal");
            yAxis = Input.GetAxis("Vertical");

            if (xAxis == 0 && yAxis == 0)
                playerState = PlayerState.Idle;
            else
                playerState = PlayerState.walking;
            if (Input.GetButtonDown("Fire2"))
            {
                playerState = PlayerState.attacking;
            }
            animator.SetInteger("PlayerState", (int)playerState);
        }

        if (yAxis > 0)
        {
            lookDirection = LookDirection.up;
        }
        else if (yAxis < 0)
        {
            lookDirection = LookDirection.down;
        }

        else if (xAxis > 0)
        {
            lookDirection = LookDirection.right;
        }

        else if (xAxis < 0)
        {
            lookDirection = LookDirection.left;
        }

        animator.SetFloat("LookDir", (float)lookDirection);
    }

    public void Hit()
    {
        StartCoroutine(FlashSprite(0.1f));
    }

    IEnumerator FlashSprite(float flashTime)
    {
        for (int i = 0; i < flashColors.Length; i++)
        {
            playerRenderer.material.color = flashColors[i];
            yield return new WaitForSeconds(flashTime);
        }
    }

    public void TriggerDeathAnimation()
    {
        playerState = PlayerState.dead;
        if (animator == null) Debug.Log("no animator");
        animator.SetTrigger("Dying");
        animator.SetInteger("PlayerState", (int)playerState);
    }
    public LookDirection GetDirection()
    {
        return lookDirection;
    }
}
