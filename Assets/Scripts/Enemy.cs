using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    Player player;

    private float poisonedHealth = 0.002f;
    private bool isPoisoned = false; // Flag to check if the player is currently poisoned


    private ParticleSystem particleEffect;


    // Initialize variables for speed, player, and inactive status
    public float speed = 2f;

    public GameObject explosionEffect;
    private AudioClip dyingSound;

    private GameObject flashEffect;
    //public GameObject prefab;
    //bool inactive = false;
    // Set timer variables
    float timer = 0f;
    float timeInterval = 10f;



    public float duration = 3f;
    public float maxIntensity = 5f;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "PoisonedEnemy")
        {
            particleEffect = transform.Find("PoisonEffect").GetComponent<ParticleSystem>();
        }
        flashEffect = GameObject.Find("FlashEffect");


        // Find the player object
        player = FindObjectOfType<Player>();

        // Set the speed based on the current game time
        SetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPoisoned)
        {
            StartCoroutine(PoisonPlayer());
        }

        // Start poisoning the player

        // If the enemy is not inactive, move towards the player object
        if (!gameObject.CompareTag("Inactive_enemy"))
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        else
        {
            // Check if the enemy is offscreen
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height)
            {
                // Destroy the enemy object
                Destroy(gameObject);
            }
        }

        // Increment the timer and update the speed if the interval has passed
        timer += Time.deltaTime;
        if (timer >= timeInterval)
        {
            SetSpeed();
            timer = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // gameObject.CompareTag("Enemy")
        // If the enemy is not inactive
        if (gameObject.tag != "Inactive_enemy")
        {
            // If the enemy falls in the hole, destroy the enemy object and add score
            if (other.tag == "Hole")
            {
                if (SceneManager.GetActiveScene().name == "Game")
                {
                    player.AddScore();
                }

                if (gameObject.tag == "Enemy" || gameObject.tag == "SmokeEnemy" || gameObject.tag == "FlashEnemy" || gameObject.tag == "PoisonedEnemy")
                {
                    DestroyEnemy();
                }

                else if (gameObject.tag == "HealFriend")
                {
                    player.HealPlayer();
                    DestroyEnemy();
                }
                else if (gameObject.tag == "ElectricEnemy")
                {
                    player.DestroyChildEnemies();
                    DestroyEnemy();
                }



            }
            // If the enemy collides with the player or another enemy, attach to the player and set speed to 0
            else if (other.tag == "Player" || other.tag == "Inactive_enemy")
            {
                if (SceneManager.GetActiveScene().name == "Game")
                {
                    player.TakeDamage();
                }

                if (gameObject.tag == "FlashEnemy")
                {
                    flashEffect.GetComponent<FlashEffect>().Flash();
                    DestroyEnemy();
                }

                else if (gameObject.tag == "HealFriend")
                {
                    DestroyEnemy();
                }

                else if (gameObject.tag == "PoisonedEnemy")
                {
                    isPoisoned = true; // Set the flag to indicate that the player is being poisoned
                }

                else if (gameObject.tag == "ElectricEnemy")
                {
                    DestroyEnemy();
                }

                speed = 0;
                transform.parent = player.transform;

                gameObject.tag = "Inactive_enemy";
            }

            else if (other.tag == "Enemy" || other.tag == "FlashEnemy")
            {
                DestroyEnemy();
            }


        }
    }



    IEnumerator PoisonPlayer()
    {
        int timer_poisoned = 0;

        for (int i = timer_poisoned; i < 10; i++)
        {
            GameObject canvasObject = GameObject.Find("Canvas");
            Slider healthBar = canvasObject.transform.Find("Slider_healthBar").GetComponent<Slider>();
            healthBar.SetValueWithoutNotify(player.health);
            player.health -= poisonedHealth; // Decrease health by 1
            yield return new WaitForSeconds(1);
            if (player.health <= 1.0f)
            {
                player.health = 1.0f;
                isPoisoned = false;
                StopCoroutine(PoisonPlayer());
                StopParticleEffect();
            }// Wait for 1 second
        }
        isPoisoned = false;
        StopCoroutine(PoisonPlayer());
        StopParticleEffect();
    }

    public void StopParticleEffect()
    {
        if (particleEffect != null)
        {
            particleEffect.gameObject.SetActive(false);
        }
    }




    public void DestroyEnemy()
    {
        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //AudioSource.PlayClipAtPoint(dyingSound, transform.position);
        effect.transform.parent = player.transform;
        Renderer renderer = effect.GetComponent<Renderer>();
        renderer.sortingLayerName = "Effect";
        Destroy(effect, 10.0f);
        Destroy(gameObject);
    }

    // Function to set the speed based on the current game time
    void SetSpeed()
    {
        float gameTime = Time.timeSinceLevelLoad;

        // Set the speed based on the game time
        if (gameTime >= 10f)
        {
            int dec = 80;
            // if (gameTime >= 100f) //275f
            // { 
            //     dec = 10;
            // }
            double timeInterval = (double)(gameTime / dec);

            speed = (float)(double)(timeInterval + 1);
            if (speed >= 4.5f)
            {
                speed = 4.5f;
            }
        }
    }
}