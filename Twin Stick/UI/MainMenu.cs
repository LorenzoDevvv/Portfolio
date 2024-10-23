using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // loads the next scene in the build index
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        Cursor.visible = true;
    }
}
    