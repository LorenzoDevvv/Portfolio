using System.Collections;
using TMPro;
using UnityEngine;

public class AnimationControls : MonoBehaviour
{
    [Header("Animators")]
    public Animator[] animators;
    public string[] firstAnimations;
    public string[] secondAnimations;
    public float animationDuration = 1.5f;
    public float startDelay = 5f; // delay before sequence starts

    [Header("UI Text")]
    public TextMeshProUGUI[] textElements; // array of texts to show
    public float textDisplayTime = 3f;     // time before text disappears

    void Start()
    {
        // Start the animation sequence
        StartCoroutine(PlayAllSequentially());

        // Start the text removal sequence
        foreach (TextMeshProUGUI text in textElements)
        {
            StartCoroutine(RemoveTextAfterSeconds(text, textDisplayTime));
        }
    }

    IEnumerator PlayAllSequentially()
    {
        yield return new WaitForSeconds(startDelay); // wait before starting

        for (int i = 0; i < animators.Length; i++)
        {
            Animator anim = animators[i];

            // Play first animation
            anim.Play(firstAnimations[i]);
            yield return new WaitForSeconds(animationDuration);

            // Play second animation
            anim.Play(secondAnimations[i]);
            yield return new WaitForSeconds(animationDuration);
        }
    }

    IEnumerator RemoveTextAfterSeconds(TextMeshProUGUI text, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        text.text = ""; // clear the text
        // Optional: disable the GameObject instead: text.gameObject.SetActive(false);
    }
}