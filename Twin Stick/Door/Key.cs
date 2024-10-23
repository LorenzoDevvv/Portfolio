using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;

    public enum KeyType
    {
        //ophalen welke kleuren aan keys er zijn
        Red,
        Green,
        Purple
    }

    public KeyType GetKeyType()
    {
        return keyType;
    }
}
