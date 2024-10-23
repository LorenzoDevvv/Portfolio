using System.Collections;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public float switchCooldown = 0.5f; // Cooldown duration between switches
    private bool canSwitch = true; // Indicates whether switching is currently allowed
    public WeaponUI weaponUI; // Reference to the WeaponUI script
    AmmoPickup ammoPickup;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        if (canSwitch)
        {
            int previousSelectedWeapon = selectedWeapon;

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                selectedWeapon++;
                if (selectedWeapon >= transform.childCount)
                    selectedWeapon = 0;

                StartCoroutine(SwitchCooldown());
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                selectedWeapon--;
                if (selectedWeapon < 0f)
                    selectedWeapon = transform.childCount - 1;

                StartCoroutine(SwitchCooldown());
            }

            if (previousSelectedWeapon != selectedWeapon)
            {
                SelectWeapon();
            }
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                Gun activeGun = weapon.GetComponent<Gun>();
                UpdateAmmoDisplay(activeGun);

                int correctedWeaponID = selectedWeapon + 1;
                if (correctedWeaponID >= transform.childCount)
                    correctedWeaponID = 0;
                if (weaponUI != null)
                {
                    weaponUI.UpdateSelectedItemUI(correctedWeaponID); // Pass the corrected weapon ID
                }
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    void UpdateAmmoDisplay(Gun activeGun)
    {
        ammoPickup = FindObjectOfType<AmmoPickup>();
        AmmoDisplay ammoDisplay = FindObjectOfType<AmmoDisplay>();
        if (ammoDisplay != null)
        {
            ammoDisplay.UpdateAmmo(activeGun.maxAmmo);
            activeGun.OnBulletShot += ammoDisplay.UpdateAmmo;
            
        }
        if (ammoPickup != null)
        {
            ammoPickup.UpdateAmmoCount(activeGun.maxAmmo);


        }
    }

    IEnumerator SwitchCooldown()
    {
        canSwitch = false;
        yield return new WaitForSeconds(switchCooldown);
        canSwitch = true;
    }
}
