using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] audioTracks;
    public AudioSource audioSource;
    private int currentTrackIndex;

    public Button prevButton;
    public Button nextButton;

    private void Start()
    {
        audioSource = GetComponent <AudioSource>();
        // Randomly select the first track in the array
        currentTrackIndex = Random.Range(0, audioTracks.Length);
        PlayTrack(currentTrackIndex);

        // Add listeners to the previous and next buttons
        prevButton.onClick.AddListener(PlayPrevTrack);
        nextButton.onClick.AddListener(PlayNextTrack);

        // Start the coroutine for auto track switching

    }

    private void Update()
    {
        // Check if the track has finished playing
        //if (audioSource.time >= audioSource.clip.length && Time.timeScale != 0.0f)
        //{
          //  PlayNextTrack();
        //}
    }

    private void PlayTrack(int index)
    {
        // Set the new track as the AudioClip for the Audio Source
        audioSource.clip = audioTracks[index];
        // Play the new track
        audioSource.Play();
        
        // Update the current track index
        currentTrackIndex = index;
    }

    private void PlayPrevTrack()
    {
        // Calculate the index of the previous track
        int prevTrackIndex = (currentTrackIndex + audioTracks.Length - 1) % audioTracks.Length;
        // Play the previous track
        PlayTrack(prevTrackIndex);
    }

    private void PlayNextTrack()
    {
        // Calculate the index of the next track
        int nextTrackIndex = (currentTrackIndex + 1) % audioTracks.Length;
        
        // Play the next track
        PlayTrack(nextTrackIndex);
    }

   
}