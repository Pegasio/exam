using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float[] parallaxSpeeds = { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };
    public Transform[] layers = new Transform[5];
    public int numberOfCopies = 2;
    public float screenWidthInPoints = 20f;

    private Vector3[] initialPositions = new Vector3[5];
    private Renderer[] renderers = new Renderer[5];

    private void Awake()
    {
        float screenWidthInPixels = Screen.width;
        screenWidthInPoints = screenWidthInPixels / Screen.dpi * 72;
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            initialPositions[i] = layers[i].position;
            renderers[i] = layers[i].GetComponent<Renderer>();
        }
    }

    private void Update()
    {
        for (int i = 1; i < 5; i++)
        {
            float offset = Time.time * parallaxSpeeds[i];
            float backgroundWidth = renderers[i].bounds.size.x * numberOfCopies;
            float xPos = (initialPositions[i].x + offset) % backgroundWidth;
            if (xPos < 0)
            {
                xPos += backgroundWidth;
            }
            layers[i].position = new Vector3(xPos, initialPositions[i].y, initialPositions[i].z);
        }
    }
}
