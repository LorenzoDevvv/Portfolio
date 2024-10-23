using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tim : MonoBehaviour
{
   public void OpenSettings()
    {
        SceneManager.LoadScene("HelpScreen");
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("BeginScreen");
    }
}
