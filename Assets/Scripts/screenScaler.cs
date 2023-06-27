using UnityEngine;
using UnityEngine.UI;

public class screenScaler : MonoBehaviour
{
    public Slider slider;
    public TMPro.TextMeshProUGUI resolutionText;
    private float resolutionScale = 1.0f;

    void Start()
    {
        slider.onValueChanged.AddListener(UpdateResolutionScale);
        UpdateResolutionText();
    }

    void UpdateResolutionScale(float value)
    {
        resolutionScale = value;
        UpdateResolutionText();
        ApplyResolutionScale();
    }

    void UpdateResolutionText()
    {
        resolutionText.text = $"Разрешение экрана: {resolutionScale:F1}";
    }

    void ApplyResolutionScale()
    {
        int width = (int)(Screen.currentResolution.width * resolutionScale);
        int height = (int)(Screen.currentResolution.height * resolutionScale);
        bool fullscreen = Screen.fullScreen;
        Screen.SetResolution(width, height, fullscreen);
    }
}
