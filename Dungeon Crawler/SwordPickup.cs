using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    private Sprite icon;
    private void Start()
    {
        icon = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Equipment>().SetIconA(icon);
            collision.gameObject.GetComponent<Attack>().EnableSword();
            Destroy(gameObject);
        }
    }
}
