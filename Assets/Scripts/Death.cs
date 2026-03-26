using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject deathUI;
    [SerializeField] Image jumpscareFace;
    [SerializeField] Image background;
    [SerializeField] Image retrySkull;
    private basicMove controller;

    public IEnumerator Die()
    {
        jumpscareFace.gameObject.SetActive(true);
        background.gameObject.SetActive(true);

        yield return StartCoroutine(Fade(jumpscareFace, 3, false));
        yield return new WaitForSeconds(1f);
        deathUI.SetActive(true);
        retrySkull.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(retrySkull, 2, true));
        yield break;
    }

    private IEnumerator Fade(Image image, int endTime, bool visible)
    {
        float elapsedTime = 0f;
        Color originalColor = image.color;
        float startAlpha = visible ? 0f : 1f;
        float targetAlpha = visible ? 1f : 0f;

        while (elapsedTime < endTime)
        {
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / endTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }
}
