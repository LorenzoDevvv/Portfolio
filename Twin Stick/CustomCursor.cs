using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Transform cursorVisual;
    public Vector3 displacement;
    PauseMenu pauseMenu;
    public bool showCursor;
    void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();
        //Cursor.visible = false;
    }


    void Update()
    {
        //if (!PauseMenu.isPaused || showCursor)
        //{
        //    gameObject.SetActive(true);
        Cursor.visible = false;
        cursorVisual.position = Input.mousePosition + displacement;

        //}
        //else
        //{
        //    gameObject.SetActive(false);

        //}

        //if (PauseMenu.isPaused)
        //{
        //    gameObject.SetActive(false);

        //}
        //else
        //{
        //    gameObject.SetActive(true);

        //}

    }
}
