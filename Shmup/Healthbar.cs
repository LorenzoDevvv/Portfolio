using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;
    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = sprites[player.health];
    }
}
