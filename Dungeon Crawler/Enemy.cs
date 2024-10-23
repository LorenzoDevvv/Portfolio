using Newtonsoft.Json;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float timerPerTile = 1f;
    protected Rigidbody2D rb;
    private EnemyState state;
    [SerializeField] protected LookDirection direction;

    protected Transform player;

    private Vector2 startPosition;
    private Vector2 targetposition;
    private float distanceToLerp;
    private float lerpTimer;

    private Camera renderCam;
    private Animator animator;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderCam = GameObject.Find("RenderCam").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void UpdateAnimator()
    {
        if(animator != null)
        {
            animator.SetInteger("State", (int)state);
            animator.SetFloat("LookDir", (float)direction);
        }
        else
        {
            Debug.LogError("No animator found on " + name);
        }
    }

    protected virtual void Attack()
    {
        state = EnemyState.attacking;
    }
    protected bool IsInsideViewport(Vector2 position)
    {
        Vector2 viewportPoint = renderCam.WorldToViewportPoint(position);
        return (viewportPoint.x > 0f && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 0.7f    );
    }
    protected void SetDestination(Vector2 destination)
    {
        startPosition = transform.position;
        targetposition = destination;

        distanceToLerp = Vector2.Distance(startPosition, destination);

        lerpTimer = 0;
        state = EnemyState.roaming;
    }

    private void MoveToDestination()
    {
            lerpTimer += Time.deltaTime;

        if (lerpTimer > (timerPerTile * distanceToLerp))
        {
            lerpTimer = (timerPerTile * distanceToLerp);
        }

        if (!distanceToLerp.Equals(0f) && !timerPerTile.Equals(0f))
        {
            float perc = lerpTimer / (timerPerTile * distanceToLerp);
            rb.MovePosition(Vector3.Lerp(startPosition, targetposition, perc));
        }

        if (lerpTimer.Equals(timerPerTile * distanceToLerp))
        {
            ReachedDestination();
        }
    }

    protected virtual void ReachedDestination()
    {
        state = EnemyState.Idle;
    }

    private void FixedUpdate()
    {
        if (state == EnemyState.roaming)
        {
            MoveToDestination();
        }

        UpdateAnimator();
    }
}
