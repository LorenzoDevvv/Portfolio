using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GravityVisualizer : MonoBehaviour
{
    public Transform sun;
    public float gravityScale = 1000f;
    private Rigidbody rb;
    public bool gravityEnabled = true;

    public bool startWithOrbit = true;

    public TextMeshProUGUI formulaText;
    public float delayBeforeShowingFormula = 4f;


    private LineRenderer lineRenderer;
    private List<Vector3> orbitPositions = new List<Vector3>();
    public int maxPoints = 500; // aantal punten in baan

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;

        if (startWithOrbit)
        {
           
            Vector3 directionToSun = sun.position - transform.position;

            
            Vector3 tangential = Vector3.Cross(directionToSun.normalized, Vector3.up);

            
            float distance = directionToSun.magnitude;
            float speed = Mathf.Sqrt(gravityScale / distance);

            
            rb.linearVelocity = tangential * speed;
        }
    }

    void FixedUpdate()
    {
        if (gravityEnabled)
        {
            Vector3 direction = sun.position - transform.position;
            float distance = direction.magnitude;
            float forceMagnitude = gravityScale / (distance * distance);
            rb.AddForce(direction.normalized * forceMagnitude);
        }

        // Baan tekenen
        orbitPositions.Add(transform.position);
        if (orbitPositions.Count > maxPoints)
            orbitPositions.RemoveAt(0);

        lineRenderer.positionCount = orbitPositions.Count;
        lineRenderer.SetPositions(orbitPositions.ToArray());
    }

    public void TurnOffGravity() => gravityEnabled = false;
    public void TurnOnGravity() => gravityEnabled = true;
}

    //private IEnumerator ShowFormulaAfterDelay()
    //{
    //    yield return new WaitForSeconds(delayBeforeShowingFormula);

    //    // Show the UI in the center
    //    formulaText.gameObject.SetActive(true);
    //}