using TMPro; // Import TextMeshPro namespace
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro UI component
    public float startTime = 60f;  // Time in seconds
    private float timeRemaining;

    private bool isRunning = true;   // Flag to check if the timer is running

    void Start()
    {
        timeRemaining = startTime;  // Initialize timer
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
            OnTimerEnd(); // Call a custom method
        }

        void OnTimerEnd()
        {
            Debug.Log("Timer Finished!");
            // Add actions like changing scenes, playing a sound, etc.
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
