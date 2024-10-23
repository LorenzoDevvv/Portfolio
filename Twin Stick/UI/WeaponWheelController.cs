using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;

    public GameObject[] weapons;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponWheelSelected = !weaponWheelSelected;
        }

        if (weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }

        else
        {
            anim.SetBool("OpenWeaponWheel", false);

        }

        switch (weaponID)
        {
            case 0: //nothing selected
                selectedItem.sprite = noImage;
                break;

            case 1: //asault rifle
                Debug.Log("Assault rifle");
                weapons[0].SetActive(true);
                weapons[1].SetActive(false);
                weapons[2].SetActive(false);
                break;

            case 2: //shotgun
                Debug.Log("Shotgun");
                weapons[0].SetActive(false);
                weapons[1].SetActive(true);
                weapons[2].SetActive(false);
                break;


            case 3: //sniper
                Debug.Log("Sniper");
                weapons[0].SetActive(false);
                weapons[1].SetActive(false);
                weapons[2].SetActive(true);
                break;

            case 4: //Grenade launcher
                Debug.Log("Grenade launcher");
                break;
        }
    }
}
