using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour, ICollectible
{
    private PlayerMovement playerMovement;
    private Gun gun;
    private Shotgun shotgun;
    private AssaultRifle assaultRifle;
    private SniperRifle sniperRifle;
    private AmmoDisplay ammoDisplay;
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        gun = FindObjectOfType<Gun>();
        shotgun = FindObjectOfType<Shotgun>();
        assaultRifle = FindObjectOfType<AssaultRifle>();
        sniperRifle = FindObjectOfType<SniperRifle>();
        ammoDisplay = FindObjectOfType<AmmoDisplay>();
    }

    public void Collect()
    {
        WeaponSwitching weaponSwitching = FindObjectOfType<WeaponSwitching>();
        if (weaponSwitching != null)
        {
            int selectedWeaponIndex = weaponSwitching.selectedWeapon;
            Transform selectedWeapon = weaponSwitching.transform.GetChild(selectedWeaponIndex);
            Gun activeGun = selectedWeapon.GetComponent<Gun>();

            if (activeGun != null)
            {
                int ammoToAdd = 0;

                if (activeGun is Shotgun)
                {
                    ammoToAdd = 20; // Specify the amount of ammo to add for Shotgun
                }
                else if (activeGun is AssaultRifle)
                {
                    ammoToAdd = 25; // Specify the amount of ammo to add for Assault Rifle
                }
                else if (activeGun is SniperRifle)
                {
                    ammoToAdd = 5; // Specify the amount of ammo to add for Sniper Rifle
                }

                activeGun.currentAmmo += ammoToAdd;
                activeGun.maxAmmo += ammoToAdd;
                ammoDisplay.UpdateAmmo(activeGun.maxAmmo); // Update the ammo display for the active gun
            }
        }

        Destroy(gameObject);
    }



    public void UpdateAmmoCount(int ammoCount)
    {

        if (ammoDisplay != null)
        {
            ammoDisplay.UpdateAmmo(ammoCount);
        }
    }
    
}
