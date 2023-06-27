using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFinish : MonoBehaviour
{
    private Player player;
    public GameObject bg;
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameObject _loseText;
    public GameObject endMenu;

    public GameObject scoreBar;
    public GameObject healthBar;
    public GameObject scoreDisplay;
    public GameObject healthDisplay;

    public AudioSource audioSource;





    public GameObject restartButton;
    public GameObject pauseButton;
    public GameObject resumeButton;



    [SerializeField] private float _timeScaleDecreaseDuration = 1.0f;


    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (player.score >= 200)
        {
            HandleEndGame(_winText);
        }
        else if (player.health <= 0)
        {
            HandleEndGame(_loseText);
        }
    }

    private void HandleEndGame(GameObject endTextObject)
    {
        audioSource.Stop();
        StartCoroutine(SlowMoTransition());
        bg.SetActive(true);

        scoreBar.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);

        HideUIButtons();
        HideUIDisplays();

        endTextObject.SetActive(true);
        endMenu.SetActive(true);
    }

    private void HideUIButtons()
    {
        restartButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
    }

    private void HideUIDisplays()
    {
        scoreDisplay.SetActive(false);
        healthDisplay.SetActive(false);
    }


    IEnumerator SlowMoTransition()
    {
        float currentTimeScale = Time.timeScale;
        float t = 0.0f;

        while (t < _timeScaleDecreaseDuration)
        {
            t += Time.unscaledDeltaTime;
            float newTimeScale = Mathf.Lerp(currentTimeScale, 0.0f, t / _timeScaleDecreaseDuration);
            Time.timeScale = newTimeScale;
            yield return null;
        }

        Time.timeScale = 0.0f;
    }
}
