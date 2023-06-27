using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public string volumeKey = "VolumeLevel"; // key used to save and load volume level

    // Called when the slider value changes
    public void OnSliderValueChanged()
    {
        // Set the volume of the mixer to the value of the slider
        mixer.SetFloat("MasterVolume", Mathf.Log10(slider.value) * 20);

        // Save the volume level to PlayerPrefs
        PlayerPrefs.SetFloat(volumeKey, slider.value);
    }

    private void Start()
    {
        // Load the volume level from PlayerPrefs
        float volume = PlayerPrefs.GetFloat(volumeKey, 1f);

        // Set the value of the slider to the loaded volume level
        slider.value = volume;
    }
}
