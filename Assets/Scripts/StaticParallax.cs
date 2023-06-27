using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StaticParallax : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public float moreSpeed;
    private delegate void MyFunctionDelegate();
    private MyFunctionDelegate setSpeedFunction;
    private bool isGame = false;

    float singleTextureWidth;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        SetupTexture();
        moveSpeed = -moveSpeed;
        moreSpeed = -moreSpeed;

        //moreSpeed = moveSpeed;

        if (SceneManager.GetActiveScene().name == "Game")
        {
            setSpeedFunction = SetSpeedLeft;
        }
        else
        {
            setSpeedFunction = emptyFunction;
            moreSpeed = moveSpeed;
        }
    }

    void SetupTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    void Scroll()
    {
        float delta = moreSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);
    }

    void CheckReset()
    {
        if ((Mathf.Abs(transform.position.x) - singleTextureWidth) > 0)
        {
            transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
        }
    }



    void SetSpeedLeft()
    {
        float gameTime = Time.timeSinceLevelLoad;
        if (gameTime > 200f)
        {
            return;
        }

        // Set the speed based on the game time

        int dec = 50;
        // if (gameTime > 100f) //278f
        // {
        //     dec = 10; //40
        // }

        float timeInterval = (gameTime / dec);

        if (gameTime <= 200f)
        {
            moreSpeed = -timeInterval + moveSpeed;
        }

        else
        {
            moreSpeed = timeInterval + -moveSpeed;
        }
    }

    void emptyFunction() { }



    void Update()
    {
        setSpeedFunction();
        Scroll();
        CheckReset();
    }
}