using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GravityManager : MonoBehaviour
{
    public GravityVisualizer[] planets;
    public TextMeshProUGUI formulaText;
    public Button gravityButton;
    public Button continueButton;

    public float delayBeforeShowingFormula = 4f;
    public float delayBeforeContinueButton = 2f;
    private bool gravityEnabled = true;

    void Start()
    {
        gravityButton.onClick.AddListener(ToggleGravity);
        continueButton.onClick.AddListener(LoadNextScene);
        formulaText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    public void ToggleGravity()
    {
        gravityEnabled = !gravityEnabled;

        if (!gravityEnabled)
        {
            TurnOffGravity();
        }
        else
        {
            TurnOnGravity();
        }
    }

    private void TurnOffGravity()
    {
        foreach (var planet in planets)
            planet.gravityEnabled = false;

        formulaText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);

        StartCoroutine(ShowFormulaAndButton());
    }

    private void TurnOnGravity()
    {
        foreach (var planet in planets)
            planet.gravityEnabled = true;

        formulaText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    private IEnumerator ShowFormulaAndButton()
    {
        // Wait for formula
        yield return new WaitForSeconds(delayBeforeShowingFormula);
        formulaText.gameObject.SetActive(true);

        // Wait extra time before showing continue button
        yield return new WaitForSeconds(delayBeforeContinueButton);
        continueButton.gameObject.SetActive(true);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
