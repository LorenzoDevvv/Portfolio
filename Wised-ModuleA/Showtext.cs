using TMPro;
using UnityEngine;

public class Showtext : MonoBehaviour
{
    public TMP_Text popupText;   
    public float delay = 15f; 

    void Start()
    {
        popupText.gameObject.SetActive(false);
        StartCoroutine(ShowTextAfterDelay());
    }

    System.Collections.IEnumerator ShowTextAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        popupText.gameObject.SetActive(true);
    }
}
