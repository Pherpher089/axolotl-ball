using System;
using TMPro;
using UnityEngine;


public class RoundTimer : MonoBehaviour
{
    public static RoundTimer Instance;
    public TMP_Text timerText; // Drag your TMP_Text element here in the inspector
    public float roundDuration = 30.0f; // Duration for the round in seconds

    private float remainingTime;
    private bool timerRunning = false;

    public event Action TimerEnded = delegate { };
    private void Awake()
    {
        Instance = this;
        timerText.enabled = false;
    }
    private void Update()
    {
        if (timerRunning)
        {
            // Decrease remaining time
            remainingTime -= Time.deltaTime;

            // Display the time left
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            // Check if timer has ended
            if (remainingTime - Time.deltaTime <= 0)
            {
                StopTimer();
                TimerEnded.Invoke();
            }

        }
    }

    public void StartTimer()
    {
        timerText.enabled = true;
        remainingTime = roundDuration;
        timerRunning = true;
    }

    private void StopTimer()
    {
        timerRunning = false;
        remainingTime = 0;
    }
}

