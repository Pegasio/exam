using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Player player;
    public AudioSource audioSource;
    private float audioPlaybackPosition;
    public GameObject scoreBar;
    public GameObject healthBar;
    public GameObject scoreDisplay;
    public GameObject healthDisplay;
    public GameObject bg;
    private bool isReadyforGame = false;
    private bool isPaused = false;
    bool end = PlayerFinish.end;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            player = FindObjectOfType<Player>();
            isReadyforGame = true;
            end = false;
        }

        else
        {
            end = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isReadyforGame)
        {
            if (player.health > 0 && player.score < 200)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (Time.timeScale == 1f)
                    {
                        PauseButton();
                    }
                    else if (Time.timeScale == 0f)
                    {
                        ResumeButton();
                    }
                }

                else if (Input.GetKeyDown(KeyCode.R))
                {
                    StartGame();
                }
            }

        }



    }

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pausebutton;



    public void StartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");

    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void Menu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseButton()
    {
        isPaused = true;

        bg.SetActive(true);
        scoreBar.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);

        scoreDisplay.SetActive(false);
        healthDisplay.SetActive(false);

        _pauseMenu.SetActive(true);
        _pausebutton.SetActive(false);
        audioPlaybackPosition = audioSource.time;
        audioSource.Pause();
        Time.timeScale = 0.0f;



    }

    public void ResumeButton()
    {
        isPaused = false;

        bg.SetActive(false);
        scoreBar.gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);

        scoreDisplay.SetActive(true);
        healthDisplay.SetActive(true);


        _pauseMenu.SetActive(false);
        _pausebutton.SetActive(true);
        audioSource.time = audioPlaybackPosition;
        audioSource.UnPause();
        Time.timeScale = 1.0f;

    }

    public void StartNextSong()
    {
        bg.SetActive(false);
        scoreBar.gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);

        scoreDisplay.SetActive(true);
        healthDisplay.SetActive(true);


        _pauseMenu.SetActive(false);
        _pausebutton.SetActive(true);
        audioSource.time = 0.0f;
        audioSource.Play();
        Time.timeScale = 1.0f;
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus) // Если игрок выходит из вкладки с игрой то:
        {
            if (!isPaused) // Если игрок не поставил игру на паузу самостоятельно - ставим на паузу
            {
                isPaused = true;
                audioPlaybackPosition = audioSource.time;
                audioSource.Pause();
                Time.timeScale = 0.0f;

            }

            else // иначе просто отсавляем паузу
            {
                PauseButton();
            }


        }

        else // если игрок возвращается во вкладку с игрой то:
        {
            if (!end && SceneManager.GetActiveScene().name == "Game") // если игра не окончена то:
            {
                ResumeButton();
            }
            
            else if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                isPaused = false;
                audioSource.time = audioPlaybackPosition;
                audioSource.UnPause();
                Time.timeScale = 1.0f;
            }
        }


    }
   
}
