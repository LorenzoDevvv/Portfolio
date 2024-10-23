using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;

    [SerializeField] private AudioClip itemSound;
    [SerializeField] private AudioClip secretSound;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            effectSource.PlayOneShot(itemSound);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            effectSource.PlayOneShot(secretSound);
        }
    }
}
