using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class SliderInteraction : MonoBehaviour
{
    [Header("Sliders to Track")]
    public Slider distanceSlider;
    public Slider mass1Slider;
    public Slider mass2Slider;

    [Header("Continue Button")]
    public Button continueButton;
    public float delayBeforeButtonAppears = 0.5f;

    private bool distanceUsed = false;
    private bool mass1Used = false;
    private bool mass2Used = false;
    private bool buttonShown = false;
    private bool initialized = false;

    void Start()
    {
        continueButton.gameObject.SetActive(false);
        StartCoroutine(InitializeAfterFrame());
        continueButton.onClick.AddListener(GoToNextScene);
    }

    private IEnumerator InitializeAfterFrame()
    {
        // Wait one frame to let sliders finish their default setup
        yield return null;
        initialized = true;

        // Now safely start listening for *real* user changes
        distanceSlider.onValueChanged.AddListener(delegate { OnSliderUsed("distance"); });
        mass1Slider.onValueChanged.AddListener(delegate { OnSliderUsed("mass1"); });
        mass2Slider.onValueChanged.AddListener(delegate { OnSliderUsed("mass2"); });
    }

    void OnSliderUsed(string sliderName)
    {
        // Ignore value changes that happen before Start() fully completes
        if (!initialized) return;

        switch (sliderName)
        {
            case "distance": distanceUsed = true; break;
            case "mass1": mass1Used = true; break;
            case "mass2": mass2Used = true; break;
        }

        if (!buttonShown && distanceUsed && mass1Used && mass2Used)
        {
            StartCoroutine(ShowButtonAfterDelay());
        }
    }

    private IEnumerator ShowButtonAfterDelay()
    {
        buttonShown = true;
        yield return new WaitForSeconds(delayBeforeButtonAppears);
        continueButton.gameObject.SetActive(true);
    }

    private void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
