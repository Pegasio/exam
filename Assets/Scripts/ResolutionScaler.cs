using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionScaler : MonoBehaviour
{
    public float scaleFactor = 1f;
    public Button decreaseButton;
    public Button increaseButton;

    void Awake()
    {
        AdjustResolution();
        decreaseButton.onClick.AddListener(DecreaseScale);
        increaseButton.onClick.AddListener(IncreaseScale);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // press R key to reset resolution to default
        {
            ResetResolution();
        }
    }

    void AdjustResolution()
    {
        int targetWidth = Mathf.RoundToInt(Screen.currentResolution.width * scaleFactor);
        int targetHeight = Mathf.RoundToInt(Screen.currentResolution.height * scaleFactor);

        Screen.SetResolution(targetWidth, targetHeight, true);
    }

    void ResetResolution()
    {
        scaleFactor = 1f;
        AdjustResolution();
    }

    void IncreaseScale()
    {
        scaleFactor += 0.1f;
        AdjustResolution();
    }

    void DecreaseScale()
    {
        scaleFactor -= 0.1f;
        AdjustResolution();
    }
}
