
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOptions
{
    public Color newColor;
    public Color oldColor;
    public bool returnOnEnd = true;
    public float fadeInTime = .01f;
    public float fadeOutTime = .01f;
}

public class BlinkColorOptions
{
    public Color Color1;
    public Color Color2;
    public float Time = .05f;
    public bool returnOnEnd = true;
}

public class SpecialFX : MonoBehaviour
{
    SpriteRenderer renderer;

    public float FadeFrequency = 2f;
    public float BlinkFrequency = 2f;

    private float lastFadeTime = 0f;
    private float lastBlinkTime = 0f;

    public static BlinkColorOptions GetDefaultOptionsBlink(SpriteRenderer renderer)
    {
        var fadeOptions = new BlinkColorOptions();
        fadeOptions.Color1 = renderer.color;
        fadeOptions.Color2 = Color.gray;
        return fadeOptions;
    }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Fade(FadeOptions options)
    {
        if (Time.time - lastFadeTime > FadeFrequency)
        {
            lastFadeTime = Time.time;
            options.oldColor = renderer.material.color;
            StartCoroutine(FadeFx(options));
        }
    }

    public void BlinkColor(BlinkColorOptions options)
    {
        if (Time.time - lastBlinkTime > BlinkFrequency)
        {
            lastBlinkTime = Time.time;
            StartCoroutine(BlinkColorFx(options));
        }
    }

    IEnumerator BlinkColorFx(BlinkColorOptions options)
    {
        yield return new WaitForSeconds(options.Time);
        renderer.material.color = Color.Lerp(options.Color2, options.Color1, options.Time);
        yield return new WaitForSeconds(options.Time);
        if (!options.returnOnEnd) yield break; // return false?
        renderer.material.color = Color.Lerp(options.Color1, options.Color2, options.Time);
        yield return new WaitForSeconds(options.Time);
    }

    IEnumerator FadeFx(FadeOptions options)
    {
        for (float f = 1f; f >= 0.5; f -= 0.1f)
        {
            options.newColor.a = f;
            renderer.material.color = options.newColor;
            yield return new WaitForSeconds(options.fadeInTime);
        }

        if (!options.returnOnEnd) yield break; // return false?

        for (float f = 0.5f; f <= 1; f += 0.1f)
        {
            options.oldColor.a = f;
            renderer.material.color = options.oldColor;
            yield return new WaitForSeconds(options.fadeOutTime);
        }
    }
}