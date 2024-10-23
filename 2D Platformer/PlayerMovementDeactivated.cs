using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementDeactivated : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] public LayerMask groundLayer;
    [SerializeField] Transform feet;

    float m_timer = 1f;

    private SpriteRenderer sprite;

    float doubleTapTime;
    KeyCode lastKeyCode;

    public float dashSpeed;
    private float dashCount;
    public float startDashCount;
    private int side;

    private enum MovementState { idle, running, jumping, falling }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        rb.freezeRotation = true;

        dashCount = startDashCount;
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();

        //dashing
        if (side == 0)
        {
            if (Input.GetKeyDown(KeyCode.A) && IsGrounded() && m_timer <= 0)
            {

                if (doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
                {
                    side = 1;
                    m_timer = 1f;
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }

                lastKeyCode = KeyCode.A;
            }
            else if (Input.GetKeyDown(KeyCode.D) && IsGrounded() && m_timer <= 0)
            {

                if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
                {
                    side = 2;
                    m_timer = 1f;
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }

                lastKeyCode = KeyCode.D;
            }
        }
        else
        {
            if (dashCount <= 0)
            {
                side = 0;
                dashCount = startDashCount;
                rb.velocity = Vector2.zero;
            }

            else
            {
                dashCount -= Time.deltaTime;

                if (side == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;

                }
                else if (side == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;

                }
            }

        }
        m_timer -= Time.deltaTime;
    }
    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }

        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
