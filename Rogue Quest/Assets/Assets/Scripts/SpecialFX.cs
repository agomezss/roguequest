
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOptions
{
    public Color newColor;
    public Color oldColor;
    public bool returnOnEnd = true;
    public float fadeInTime = .01f;
    public float fadeOutTime  = .01f;
}

public class SpecialFX : MonoBehaviour
{
    SpriteRenderer renderer;

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Fade(FadeOptions options)
    {
        options.oldColor = renderer.material.color;
        StartCoroutine(FadeFx(options));
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