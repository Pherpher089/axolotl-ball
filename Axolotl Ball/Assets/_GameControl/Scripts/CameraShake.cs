using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    public float shakeDuration = 0.5f; // Duration of camera shaking
    public float shakeMagnitude = 0.1f; // Extent of camera shaking
    public float slowMotionFactor = 0.5f; // The scale at which time is slowed (0.5f means half-speed)
    public float slowMotionDuration = 0.2f; // Duration of slow-motion effect

    private Vector3 originalPos;
    private float currentShakeDuration;

    private void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        if (currentShakeDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;
            currentShakeDuration -= Time.unscaledDeltaTime;
            Time.timeScale = slowMotionFactor;
        }
        else
        {
            Time.timeScale = 1.0f;
            transform.localPosition = originalPos;
        }
    }

    public void TriggerShake()
    {
        currentShakeDuration = shakeDuration;
        Time.timeScale = slowMotionFactor;
        Invoke("RestoreTime", slowMotionDuration);
    }

    void RestoreTime()
    {
        Time.timeScale = 1.0f;
    }
}
