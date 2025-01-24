using System;
using TMPro; // Import TextMeshPro namespace
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro UI component
    private float timeRemaining;

    private bool isRunning = false;   // Flag to check if the timer is running
    public Action OnTimerEnd { get; set; }

    void Start()
    {
        timeRemaining = 0;  // Initialize timer
    }

    public void StartTimer(float startTime)
    {
        timeRemaining = startTime;  // Initialize timer
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            // Reduce time
            timeRemaining -= Time.deltaTime;

            // Clamp to zero to prevent negative time
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isRunning = false; // Stop the timer
            }

            // Update the UI
            UpdateTimerDisplay();
        }

        if (timeRemaining <= 0)
        {
            isRunning = false;
            OnTimerEnd?.Invoke(); // Call a custom method
        }
    }

    void UpdateTimerDisplay()
    {
        // Format the time as MM:SS
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
