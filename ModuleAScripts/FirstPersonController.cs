using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    [Header("References")]
    public Transform playerCamera;
    public CharacterController controller;

    private float xRotation = 0f;
    private bool canMove = true; // controls player movement

    void Start()
    {
        // Cursor starts locked and invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleLook();
        HandleMovement();
        HandleWorldSpaceUI();
        HandleInputFieldCursor();
    }

    void HandleLook()
    {
        if (!canMove) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        if (!canMove) return;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void HandleWorldSpaceUI()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = new Vector2(Screen.width / 2f, Screen.height / 2f);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult r in results)
        {
            // Buttons
            if (r.gameObject.GetComponent<UnityEngine.UI.Button>() != null)
            {
                if (Input.GetMouseButtonDown(0))
                    r.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            }

            // InputFields
            if (r.gameObject.GetComponent<TMP_InputField>() != null ||
                r.gameObject.GetComponent<UnityEngine.UI.InputField>() != null)
            {
                if (Input.GetMouseButtonDown(0))
                    EventSystem.current.SetSelectedGameObject(r.gameObject);
            }
        }
    }

    void HandleInputFieldCursor()
    {
        // Check if an InputField is selected
        var selected = EventSystem.current.currentSelectedGameObject;
        bool inputSelected = selected != null &&
            (selected.GetComponent<TMP_InputField>() != null || selected.GetComponent<UnityEngine.UI.InputField>() != null);

        if (inputSelected)
        {
            // Disable player movement
            canMove = false;
            // Unlock cursor (so Unity knows mouse position) but keep it invisible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
        else
        {
            // Enable player movement
            canMove = true;
            // Lock cursor and keep it invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
