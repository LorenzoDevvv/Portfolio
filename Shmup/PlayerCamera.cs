using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float movingSpeed = 0.5f;

    void Start()
    {
        

    }

    
    void Update()
    {
        transform.position = Vector3.right * Time.deltaTime * movingSpeed;

    }
}