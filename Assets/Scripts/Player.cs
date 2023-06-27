using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Player : MonoBehaviour
{

    public float turnSpeed = -200;
    public float health = 50;
    public int score = 0;
    private float waitTime = 0.6f;
    public Slider scoreBar;
    public Slider healthBar;
    Enemy enemy;

    bool isBlinking = false;



    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetValueWithoutNotify(health);
        scoreBar.SetValueWithoutNotify(score);
    }

    // Update is called once per frame
    void Update()
    {


        if (Time.timeSinceLevelLoad > 278f)
        {
            turnSpeed = -300;
        }

        else if (Time.timeSinceLevelLoad > 100f)
        {
            turnSpeed = -250;
        }


        if (health >= 31)
        {
            StopCoroutine(BlinkSlider());
            healthBar.fillRect.GetComponent<Image>().color = Color.green;
        }
        else if (health >= 11)
        {
            StopCoroutine(BlinkSlider());
            healthBar.fillRect.GetComponent<Image>().color = Color.yellow;
        }

        else if (health <= 10)
        {
            StartCoroutine(BlinkSlider());
        }

        else
        {
            healthBar.fillRect.GetComponent<Image>().color = Color.red;
        }



        transform.Rotate(Vector3.forward * turnSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime);
        transform.Rotate(Vector3.forward * turnSpeed * GetHorizontalInput() * Time.deltaTime);


    }

    private int GetHorizontalInput()
    {
        int direction = 0;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    direction -= 1;
                }
                else
                {
                    direction += 1;
                }
            }
        }

        if (Mathf.Abs(direction) > 1)
        {
            return 0; // return 0 speed if both left and right are pressed at the same time
        }
        else
        {
            return direction;
        }
    }


    IEnumerator BlinkSlider()
    {
        if (!isBlinking && health <= 10)
        {
            while (health <= 10)
            {
                if (health == 1)
                {
                    waitTime = 0.2f;
                }

                else if (health <= 5)
                {
                    waitTime = 0.4f;
                }

                else
                {
                    waitTime = 0.6f;
                }
                isBlinking = true;
                // Blink the slider by alternating its color between red and transparent
                var fillColor = healthBar.fillRect.GetComponent<Image>().color = Color.red;
                healthBar.fillRect.GetComponent<Image>().color = new Color(fillColor.r, fillColor.g, fillColor.b, 0.5f);
                yield return new WaitForSeconds(waitTime);
                healthBar.fillRect.GetComponent<Image>().color = new Color(fillColor.r, fillColor.g, fillColor.b, 1f);
                yield return new WaitForSeconds(waitTime);
            }
        }

        else if (isBlinking && health > 10)
        {
            isBlinking = false;
            StopCoroutine(BlinkSlider());
        }

    }

    public void DestroyChildEnemies()
    {
        // Get all child objects of the player object
        Transform playerTransform = GetComponent<Transform>();
        int childCount = playerTransform.childCount;

        // Destroy each child object that has an Enemy component
        for (int i = 0; i < childCount; i++)
        {
            Transform child = playerTransform.GetChild(i);
            Enemy enemy = child.GetComponent<Enemy>();
            if (child.GetComponent<Enemy>() != null)
            {
                enemy.DestroyEnemy();
                // Destroy(child.gameObject);
            }
        }
    }



    public void HealPlayer()
    {
        health += 5;
        if (health > 50)
        {
            score += 5;
        }
        healthBar.SetValueWithoutNotify(health);
    }

    public void TakeDamage()
    {
        health--;
        healthBar.SetValueWithoutNotify(health);
    }

    public void AddScore()
    {
        score++;
        scoreBar.SetValueWithoutNotify(score);

        if (score >= 90)
        {
            scoreBar.fillRect.GetComponent<Image>().color = Color.green;
        }
        else if (score >= 25)
        {
            scoreBar.fillRect.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            scoreBar.fillRect.GetComponent<Image>().color = Color.white;
        }
    }
}
