using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.ShaderData;

public static class TextureExtensions
{
    public static void Clear(this Texture2D tex, Color col)
    {
        Color[] fill = tex.GetPixels();
        for (int i = 0; i < fill.Length; i++)
            fill[i] = col;
        tex.SetPixels(fill);
    }
}

public class GravityAdjuster : MonoBehaviour
{
    [Header("Transforms")]
    public Transform earth;
    public Transform moon;

    [Header("Sliders")]
    public Slider distanceSlider;
    public Slider mass1Slider;
    public Slider mass2Slider;

    [Header("UI Display")]
    public TMP_Text distanceLabel;
    public TMP_Text mass1Label;
    public TMP_Text mass2Label;
    public TMP_Text forceLabel;

    [Header("Graph Elements")]
    public RawImage graphPanel;
    public RectTransform marker;
    public RectTransform xLabelsParent;
    public RectTransform yLabelsParent;
    public TMP_Text labelPrefab;

    [Header("Settings")]
    public float yMax = 20000f;
    public float timeWindow = 10f;

    private Texture2D graphTexture;
    private float currentDistance;
    private const float G = 16000f;
    private List<Vector2> forceHistory = new List<Vector2>(); // (tijd, kracht)
    private float elapsedTime = 0f;
    private List<TMP_Text> xLabels = new List<TMP_Text>();
    private List<TMP_Text> yLabels = new List<TMP_Text>();

    void Start()
    {
        SetupSliders();
        InitGraph();
        CreateAxisLabels();
        UpdateSimulation();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateSimulation();
        UpdateAxisLabels();
        UpdateMoonMovement();
    }

    void SetupSliders()
    {
        distanceSlider.minValue = 8f;
        distanceSlider.maxValue = 24f;
        distanceSlider.value = 12f;
        distanceSlider.onValueChanged.AddListener(delegate { UpdateSimulation(); });

        mass1Slider.minValue = 3;
        mass1Slider.maxValue = 8;
        mass1Slider.value = 5;
        mass1Slider.wholeNumbers = true;
        mass1Slider.onValueChanged.AddListener(delegate { UpdateSimulation(); });

        mass2Slider.minValue = 3;
        mass2Slider.maxValue = 8;
        mass2Slider.value = 5;
        mass2Slider.wholeNumbers = true;
        mass2Slider.onValueChanged.AddListener(delegate { UpdateSimulation(); });
    }

    float GetMass(float sliderValue)
    {
        return sliderValue;
    }

    void UpdateSimulation()
    {
        currentDistance = Mathf.Max(distanceSlider.value, 0.1f);
        moon.position = earth.position + Vector3.right * currentDistance;

        float m1 = GetMass(mass1Slider.value);
        float m2 = GetMass(mass2Slider.value);

        earth.localScale = Vector3.one * m1;
        moon.localScale = Vector3.one * m2;

        float force = (G * m1 * m2) / (currentDistance * currentDistance);

        UpdateUI(force, m1, m2);
        UpdateGraph(force);
        UpdateMarker(force);
    }

    void UpdateUI(float force, float m1, float m2)
    {
        distanceLabel.text = $"{currentDistance:F1} m";
        mass1Label.text = $"{m1:F0} kg";
        mass2Label.text = $"{m2:F0} kg";
        forceLabel.text = $"{force:F0} N";
    }

    void UpdateMoonMovement()
    {
        moon.position = (moon.position - earth.position).normalized * currentDistance + earth.position;
    }

    void InitGraph()
    {
        graphTexture = new Texture2D(600, 400);
        graphTexture.filterMode = FilterMode.Point;
        graphPanel.texture = graphTexture;
        graphTexture.Clear(Color.white);
        graphTexture.Apply();
    }

    void UpdateGraph(float force)
    {
        // Voeg nieuw punt toe
        forceHistory.Add(new Vector2(elapsedTime, force));

        // Oude punten verwijderen buiten timeWindow
        while (forceHistory.Count > 0 && elapsedTime - forceHistory[0].x > timeWindow)
            forceHistory.RemoveAt(0);

        // Tekenen
        graphTexture.Clear(Color.white);

        if (forceHistory.Count < 2)
        {
            graphTexture.Apply();
            return;
        }

        float xMin = elapsedTime - timeWindow;
        float xMax = elapsedTime;

        for (int i = 1; i < forceHistory.Count; i++)
        {
            Vector2 p0 = forceHistory[i - 1];
            Vector2 p1 = forceHistory[i];

            float x0 = Mathf.InverseLerp(xMin, xMax, p0.x) * (graphTexture.width - 1);
            float x1 = Mathf.InverseLerp(xMin, xMax, p1.x) * (graphTexture.width - 1);
            float y0 = Mathf.Clamp(p0.y / yMax, 0, 1) * (graphTexture.height - 1);
            float y1 = Mathf.Clamp(p1.y / yMax, 0, 1) * (graphTexture.height - 1);

            DrawLine((int)x0, (int)y0, (int)x1, (int)y1, Color.blue);
        }

        graphTexture.Apply();
    }

    void DrawLine(int x0, int y0, int x1, int y1, Color col)
    {
        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = (x0 < x1) ? 1 : -1;
        int sy = (y0 < y1) ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            if (x0 >= 0 && x0 < graphTexture.width && y0 >= 0 && y0 < graphTexture.height)
                graphTexture.SetPixel(x0, y0, col);

            if (x0 == x1 && y0 == y1) break;

            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
    }

    void UpdateMarker(float force)
    {
        float normalizedY = Mathf.Clamp(force / yMax, 0, 1);
        marker.anchoredPosition = new Vector2(
            graphPanel.rectTransform.rect.width,
            normalizedY * graphPanel.rectTransform.rect.height
        );
    }

    void CreateAxisLabels()
    {
        // Maak 5 X-labels (tijd) en 6 Y-labels (kracht)
        for (int i = 0; i <= 5; i++)
        {
            var label = Instantiate(labelPrefab, xLabelsParent);
            xLabels.Add(label);
        }

        for (int i = 0; i <= 6; i++)
        {
            var label = Instantiate(labelPrefab, yLabelsParent);
            yLabels.Add(label);
        }
    }

    void UpdateAxisLabels()
    {
        float width = graphPanel.rectTransform.rect.width;
        float height = graphPanel.rectTransform.rect.height;

        float step = timeWindow / 5f;

        for (int i = 0; i < xLabels.Count; i++)
        {
            // X loopt van -timeWindow naar 0
            float t = -timeWindow + i * step;
            xLabels[i].text = $"{t:F1}s";
            xLabels[i].rectTransform.anchoredPosition = new Vector2((i / 5f) * width, -20f);
        }

        // Y-as (kracht in N)
        for (int i = 0; i < yLabels.Count; i++)
        {
            float val = (i / 6f) * yMax;
            yLabels[i].text = $"{val:F0} N";
            yLabels[i].rectTransform.anchoredPosition = new Vector2(-50f, (val / yMax) * height - 10f);
        }
    }
}