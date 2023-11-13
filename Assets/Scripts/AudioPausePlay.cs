using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPausePlay : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource == null) 
        { 
            audioSource = FindObjectOfType<AudioSource>(); 
        }

        if(audioSource.mute == true && audioSource.isPlaying)
        {
            Time.timeScale = 0.0f;
        }

        else if(audioSource.mute != true && audioSource.isPlaying)
        {
           Time.timeScale = 1.0f;
        }


    }
    public void PauseMusic()
    {
        audioSource.mute = true;
    }

    public void ResumeMusic()
    {
        audioSource.mute = false;
    }
}
