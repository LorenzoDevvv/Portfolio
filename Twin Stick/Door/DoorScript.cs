using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorScript : MonoBehaviour
{
    public Animator _anim;

    //If the player is in contact with the trigger collider open the door
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            _anim.SetBool("IsOpening", true);
        }    
    }

    //If the player is not in contact with the trigger collider close the door
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {         
            _anim.SetBool("IsOpening", false);           
        }
    }

    //public void OpenDoor()
    //{
    //    if (tag.Equals("Door"))
    //    {
    //        _anim.SetBool("IsOpening", true);
    //    }
    //}
}
