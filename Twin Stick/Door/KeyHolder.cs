using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyHolder : MonoBehaviour
{
    
    private List<Key.KeyType> keyList;

    private void Awake()
    {
        //list aanmaken voor alle keys
        keyList = new List<Key.KeyType>();
    }

    
    public void AddKey(Key.KeyType keyType)
    {
        keyList.Add(keyType);
    }

    public void RemoveKey(Key.KeyType keyType)
    {
        keyList.Remove(keyType);
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter(Collider other)
    {
        //on collision key toevoegen aan inventory en de key removen van de map
        Key key = other.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }
        //checken of je de juiste key heb voor de deur wanneer je voor de deur staat
        KeyDoor keyDoor = other.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
           if (ContainsKey(keyDoor.GetKeyType()))
            {
                // wanneer je de key hebt gaat de deur open
                keyDoor.OpenDoor();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //wanneer je voorbij de deur bent gaat de deur weer dicht
        KeyDoor keyDoor = other.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            keyDoor.CloseDoor();
        }
    }
}
