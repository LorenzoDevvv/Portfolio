using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    public RectTransform crosshair;

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        crosshair.position = mousePos;
    }
}