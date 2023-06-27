using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private GameObject _pauseMenu;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape) || _pauseMenu.activeInHierarchy)
        // {
        //     if (Time.timeScale == 0f)
        //     {
        //         audioSource.Pause();
        //     }
        //     else
        //     {
        //         audioSource.Play();
        //     }
        // }
    }


}
