using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;
    public Animator anim;

    //return the keytype
    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    //open the door 
    public void OpenDoor()
    {
        anim.SetBool("IsOpening", true);
    }

    //close the door
    public void CloseDoor()
    {
        anim.SetBool("IsOpening", false);
    }
}
