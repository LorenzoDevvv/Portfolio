using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    public Transform door;
    public Vector3 initialPosition;
    public Vector3 targetPosition;
    public float slideSpeed = 1f;


    private bool isMoving = false;

    private void Start()
    {
        initialPosition = door.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            StartCoroutine(MoveDoor(targetPosition));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            StartCoroutine(MoveDoor(initialPosition));

        }
    }

    public IEnumerator MoveDoor(Vector3 target)
    {
        isMoving = true;
        while (door.localPosition != target)
        {
            door.localPosition = Vector3.MoveTowards(door.localPosition, target, slideSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }

    public void OpenDoor()
    {
        Debug.Log("OpenDoor");
        StartCoroutine(MoveDoor(targetPosition));
    }
}
