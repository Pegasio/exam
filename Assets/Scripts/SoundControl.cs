using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Volume", 1f);
        audioMixer.SetFloat("Volume", Mathf.Log10(slider.value) * 20);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
