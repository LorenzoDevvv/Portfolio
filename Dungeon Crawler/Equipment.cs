using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    [SerializeField] private Image iconButton_A;
    [SerializeField] private Image iconButton_B;
    void Start()
    {
        StartInactive();
    }
    public void StartInactive()
    {
        if (iconButton_A.sprite == null)
        {
            iconButton_A.gameObject.SetActive(false);
        }
        if (iconButton_B.sprite == null)
        {
            iconButton_B.gameObject.SetActive(false);
        }
    }

    public void SetIconA(Sprite iconSprite)
    {
        iconButton_A.gameObject.SetActive(true);
        iconButton_A.sprite = iconSprite;
    }

    public void SetIconB(Sprite iconSprite)
    {
        iconButton_B.gameObject.SetActive(true);
        iconButton_B.sprite = iconSprite;
    }
}
