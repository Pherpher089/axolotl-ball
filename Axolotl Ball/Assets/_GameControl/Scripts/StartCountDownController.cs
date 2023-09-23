using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StartCountDownController : MonoBehaviour
{
    public TMP_Text[] countdownTexts; // Assign these in the Inspector in the order: 3, 2, 1, Go!
    public GameObject winScreenUI;
    public delegate void CountdownCompleteDelegate();
    public event CountdownCompleteDelegate OnCountdownComplete;
    public float slowDownFactor = 0.5f;  // to slow down time to half-speed
    public float delayBeforeWinScreen = 2f;
    public void StartCountdown()
    {
        StartCoroutine(CountdownRoutine());
    }

    public void TriggerWin()
    {
        if (winScreenUI != null)
        {
            StartCoroutine(WinSequence());
        }
    }
    private IEnumerator CountdownRoutine()
    {
        // Disable player controls here

        yield return ZoomIn(8f, 1.5f); // Zoom into a size of 3 over 1.5 seconds
        int c = 0;
        foreach (TMP_Text t in countdownTexts)
        {
            c++;
            bool isLast = c == 4 ? true : false;
            yield return FadeTextInAndOut(t, 1f, isLast); // Each text fades in and out over 1 second
        }


        // Reset camera zoom or initiate other match-starting functions here
        // Enable player controls here
    }

    private IEnumerator ZoomIn(float targetZoom, float duration)
    {
        float startZoom = Camera.main.orthographicSize;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(startZoom, targetZoom, elapsed / duration);
            yield return null;
        }
        Camera.main.orthographicSize = targetZoom;
    }

    private IEnumerator FadeTextInAndOut(TMP_Text textElement, float duration, bool lastItem = false)
    {
        // Fade in
        float elapsed = 0f;
        bool hasSounded = false;
        while (elapsed < duration / 2)
        {
            elapsed += Time.deltaTime;
            Color color = textElement.color;
            color.a = Mathf.Lerp(0f, 1f, elapsed / (duration / 2));
            textElement.color = color;
            yield return null;
            if(!hasSounded)
            {
                if (lastItem)
                {
                    SoundManager.instance.PlayStartSfx();
                    OnCountdownComplete.Invoke();
                }
                else
                {
                    SoundManager.instance.PlayCountSfx();
                }
                hasSounded = true;
            }
        }

        // Reset elapsed time for fade out
        elapsed = 0f;
        while (elapsed < duration / 2)
        {
            elapsed += Time.deltaTime;
            Color color = textElement.color;
            color.a = Mathf.Lerp(1f, 0f, elapsed / (duration / 2));
            textElement.color = color;
            yield return null;
        }

        textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, 0);
    }
    private IEnumerator WinSequence()
    {
        // Slow down time
        Time.timeScale = slowDownFactor;

        // Wait for a few seconds
        float timer = 0f;
        while (timer < delayBeforeWinScreen)
        {
            timer += Time.unscaledDeltaTime; // Unscaled time continues normally regardless of timeScale
            yield return null;
        }

        // Reset time to normal

        // Enable the win screen UI
        winScreenUI.SetActive(true);
    }
}
