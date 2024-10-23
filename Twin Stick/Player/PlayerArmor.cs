using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArmor : MonoBehaviour
{
    public int armorPoints = 5;

    [SerializeField] public Slider armorSlider;
    public bool bulletProofVestIsOn = false;


    // Update is called once per frame
    void Update()
    {
        if (armorPoints <= 0)
        {
            bulletProofVestIsOn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //tag comparen zodat je alleen de armor hit
        if (other.gameObject.CompareTag("Armor"))
        {
            bulletProofVestIsOn = true;
            armorSlider.value = 5f;
            armorPoints = 5;
            Destroy(GameObject.FindWithTag("Armor"));
        }
    }
}
