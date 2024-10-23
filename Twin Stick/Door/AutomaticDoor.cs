using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    DoorTest doorTest;
    
    private void Start()
    {
        doorTest = FindObjectOfType<DoorTest>();
    }
    public void OpenDoor()
    {
        Debug.Log("OpenDoor");
        StartCoroutine(doorTest.MoveDoor(doorTest.targetPosition));
    }
}
