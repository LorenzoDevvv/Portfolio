using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class AnswerCheck : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text questionText;
    public TMP_InputField inputField;
    public TMP_Text feedbackText;

    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] correctAnswers;
    }

    [Header("Vragenlijst")]
    public Question[] questions;

    private int currentQuestion = 0;

    void Start()
    {
        ShowQuestion();
    }

    public void CheckAnswer()
    {
        string userAnswer = inputField.text
            .Trim()
            .ToLower()
            .Replace(" ", "");

        bool isCorrect = questions[currentQuestion].correctAnswers.Any(a =>
            userAnswer == a.ToLower().Replace(" ", "")
        );

        if (isCorrect)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;

            if (currentQuestion >= questions.Length - 1)
            {
                Invoke(nameof(LoadNextScene), 1.5f);
            }
            else
            {
                Invoke(nameof(NextQuestion), 1.5f);
            }
        }
        else
        {
            feedbackText.text = "Probeer opnieuw...";
            feedbackText.color = Color.red;
        }
    }

    void ShowQuestion()
    {
        if (currentQuestion < questions.Length)
        {
            questionText.text = questions[currentQuestion].question;
            inputField.text = "";
            feedbackText.text = "";
        }
        else
        {
            questionText.text = "Alle vragen voltooid!";
            inputField.gameObject.SetActive(false);
        }
    }

    void NextQuestion()
    {
        currentQuestion++;
        ShowQuestion();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex + 1
        );
    }
}
