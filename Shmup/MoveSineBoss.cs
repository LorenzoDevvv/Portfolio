using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSineBoss : MonoBehaviour
{
    float sinCenterY;
    public float amplitude = 2;
    public float frequency = 2;
    public bool inverted = false;
    
    void Start()
    {
        sinCenterY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, Mathf.Sin(Time.time), 0);
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float sin = Mathf.Sin(pos.x * frequency) * amplitude;
        pos.y = sinCenterY + sin;
        if (inverted)
        {
            sin *= -1;
        }

        transform.position = pos;
    }
}
