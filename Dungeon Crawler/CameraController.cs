using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.TimeZoneInfo;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] Transform transform;
    [SerializeField] private float trasitionTime = 1f;
    [SerializeField] private int camDistanceHorizontal = 16;
    [SerializeField] private int camDistanceVertical = 11;
    [SerializeField] private float playerDistanceHorizontal = 0f;
    [SerializeField] private float playerDistanceVertical = 1.5f;
    [SerializeField] private Rect bounds = new Rect(0, 0, 1, 0.7f);


    private Camera cam;
    private PlayerMovement playerMovement;
    private PlayerAnimator playerAnimator;
    private Coroutine currentCoroutine;
    private Game game;
    

    void Start()
    {
        cam = GetComponent<Camera>();
        game = FindObjectOfType<Game>();
        playerMovement = transform.GetComponent<PlayerMovement>();
        playerAnimator = transform.GetComponent<PlayerAnimator>();
    }

    void Update()
    {
       Vector3 viewPortPosition = cam.ViewportToWorldPoint(transform.position);

        if (currentCoroutine != null) return;
        CheckForBorderTransitions();
       
    }

    public IEnumerator WorldCoroutine(Vector3 direction, float camDistance, float playerDistance)
    {
        playerMovement.SetPause(true);
        playerAnimator.enabled = false;
        game.DeactivateCurrentRoom();
        yield return new WaitForSeconds(0.2f);

        Vector3 camStart = base.transform.position;
        Vector3 camEnd = camStart + direction * camDistance;

        Vector3 playerStart = playerMovement.transform.position;
        Vector3 playerEnd = playerStart + direction * playerDistance;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / trasitionTime;
            base.transform.position = Vector3.Lerp(camStart, camEnd, t);
            transform.position = Vector3.Lerp(playerStart, playerEnd, t);
            yield return null;
        }

        playerAnimator.enabled = true;
        yield return new WaitForSeconds(0.2f);
        game.ActivateRoom(transform.position);
        playerMovement.SetPause(false);

        currentCoroutine = null;
    }

    private void CheckForBorderTransitions()
    {
        Vector2 viewPortPosition = cam.WorldToViewportPoint(transform.position);

        if (viewPortPosition.x > bounds.width)
        {
            currentCoroutine = StartCoroutine(WorldCoroutine(Vector3.right, camDistanceHorizontal, playerDistanceHorizontal));
        }
        else if (viewPortPosition.x < -bounds.x)
        {
            currentCoroutine = StartCoroutine(WorldCoroutine(Vector3.left, camDistanceHorizontal, playerDistanceHorizontal));
        }
        else if (viewPortPosition.y > bounds.height)
        {
            currentCoroutine = StartCoroutine(WorldCoroutine(Vector3.up, camDistanceVertical, playerDistanceVertical));
        }
        else if (viewPortPosition.y < bounds.y)
        {
            currentCoroutine = StartCoroutine(WorldCoroutine(Vector3.down, camDistanceVertical, playerDistanceVertical));
        }

    }
}
