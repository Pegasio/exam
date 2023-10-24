using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Player player;
    public AudioSource audioSource;
    public GameObject scoreBar;
    public GameObject healthBar;
    public GameObject scoreDisplay;
    public GameObject healthDisplay;
    public GameObject bg;
    private bool isReadyforGame = false;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            player = FindObjectOfType<Player>();
            isReadyforGame = true;
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
                    else if(Time.timeScale == 0f)
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
        bg.SetActive(true);
        Time.timeScale = 0.0f;
        scoreBar.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);

        scoreDisplay.SetActive(false);
        healthDisplay.SetActive(false);

        _pauseMenu.SetActive(true);
        _pausebutton.SetActive(false);
        audioSource.Pause();


    }

    public void ResumeButton()
    {
        bg.SetActive(false);
        scoreBar.gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);

        scoreDisplay.SetActive(true);
        healthDisplay.SetActive(true);

        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
        _pausebutton.SetActive(true);
        audioSource.Play();

    }
}
