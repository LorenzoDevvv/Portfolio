using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class ItemCollectCherry : MonoBehaviour
{
    private int cherryScore;
    [SerializeField] private TextMeshProUGUI CherrieText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            cherryScore += 10;
            CherrieText.text = "Cherries: " + cherryScore;
            //Debug.Log(cherryScore);
        }
    }
}

