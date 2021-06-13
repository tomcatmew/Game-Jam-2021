using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{

    public enum FadeType
    {
        Loading,
        GameOver
    }

    public CanvasGroup loadingCanvasGroup;
    public CanvasGroup gameOverCanvasGroup;
    public float fadeDuration = 1f;

    public void FadeSceneOut(FadeType fadeType)
    {
        CanvasGroup canvasGroup;
        if(fadeType.Equals(FadeType.GameOver))
        {
            canvasGroup = gameOverCanvasGroup;
        } else
        {
            canvasGroup = loadingCanvasGroup;
        }

        canvasGroup.gameObject.SetActive(true);

        Fade(1f, canvasGroup);
    }

    public void FadeSceneIn()
    {
        CanvasGroup canvasGroup;
        if (gameOverCanvasGroup.alpha > 0.1f)
            canvasGroup = gameOverCanvasGroup;
        else
            canvasGroup = loadingCanvasGroup;

        Fade(0f, canvasGroup);

        canvasGroup.gameObject.SetActive(false);
    }

    private void Fade(float finalAlpha, CanvasGroup canvasGroup)
    {
        canvasGroup.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / fadeDuration;
        while (!Mathf.Approximately(canvasGroup.alpha, finalAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);
        }
        canvasGroup.alpha = finalAlpha;
        canvasGroup.blocksRaycasts = false;
    }


}
