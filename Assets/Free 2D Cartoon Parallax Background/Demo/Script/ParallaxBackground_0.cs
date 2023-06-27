using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground_0 : MonoBehaviour
{
    public bool Camera_Move;
    public float Camera_MoveSpeed = 1.5f;
    [Header("Layer Setting")]
    public float[] Layer_Speed = new float[7];
    public GameObject[] Layer_Objects = new GameObject[7];

    private Transform _camera;
    private float[] startPos = new float[7];
    private float[] repeatWidth = new float[7];

    void Start()
    {
        _camera = Camera.main.transform;

        // Initialize the starting position and repeat width of each layer
        for (int i = 0; i < 5; i++)
        {
            startPos[i] = Layer_Objects[i].transform.position.x;
            repeatWidth[i] = Layer_Objects[i].GetComponent<SpriteRenderer>().sprite.bounds.size.x * Layer_Objects[i].transform.localScale.x;
        }
    }

    void Update()
    {
        // Moving objects
        for (int i = 0; i < 5; i++)
        {
            float distance = (_camera.position.x - startPos[i]) * Layer_Speed[i];
            float newPositionX = Mathf.Repeat(startPos[i] + distance, repeatWidth[i]);
            Layer_Objects[i].transform.position = new Vector2(newPositionX, Layer_Objects[i].transform.position.y);
        }

        // Moving camera
        if (Camera_Move)
        {
            _camera.position += Vector3.right * Time.deltaTime * Camera_MoveSpeed;
        }
    }
}
