using UnityEngine;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
    public float duration = 0.5f;
    public Color color = Color.white;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float delay = 0.5f;


    private SpriteRenderer renderer;
    private Coroutine flashStatus;

    void Start()
    {



        renderer = GetComponent<SpriteRenderer>();
    }

    public void Flash()
    {

        if (flashStatus == null)
        {
            flashStatus = StartCoroutine(FlashCoroutine());
        }
        
    }

    IEnumerator FlashCoroutine()
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            float alpha = curve.Evaluate(t / duration);
            Color flashColor = color * alpha;
            renderer.color = flashColor;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(delay);

        float u = 0f;

        while (u < duration)
        {
            u += Time.deltaTime;

            float alpha = curve.Evaluate(1f - u / duration);
            Color flashColor = color * alpha;
            renderer.color = flashColor;

            yield return new WaitForEndOfFrame();
        }

        renderer.color = Color.clear;
        yield return null;
        flashStatus = null;
    }
}