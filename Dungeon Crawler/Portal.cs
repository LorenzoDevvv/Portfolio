using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //[SerializeField] private Portal connectedPortal;

    public float x;
    public float y;

    //private Game gameManager;
    //private Transform playerTransform;
    //private bool steppedOut;
    //private float triggerTime = 0.25f;
    //private float stayTimer = 0;
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector2(x, y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    public void ReceiveObject(Transform objectTransform)
    {

    }

    private void OnDrawGizmos()
    {
        
    }
}
