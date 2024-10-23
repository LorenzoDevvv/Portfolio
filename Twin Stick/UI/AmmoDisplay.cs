using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;
    PlayerMovement playerMovement;
    private Gun gun;
    private WeaponSwitching weaponSwitching;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        gun = FindObjectOfType<Gun>();
        weaponSwitching = FindObjectOfType<WeaponSwitching>();
        UpdateAmmo(gun != null ? gun.maxAmmo : 0);


    }

    public void UpdateAmmo(int ammoCount)
    {
        if (text != null)
        {
            text.text = ammoCount.ToString();

        }
    }
}

