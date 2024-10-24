using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public GameObject bullet;
    
  
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    
    void Update()
    {

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
