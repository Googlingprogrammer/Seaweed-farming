using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    private float timeLeft; // Time left to count down
    public TextMeshProUGUI timerText; // Reference to the UI Text component

    void Start()
    {
        timeLeft = totalTime; // Set the initial time left
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime; // Decrease the time left by the time passed since the last frame

            // Update the text to display the time left as an integer
            timerText.text = "Time Left: " + Mathf.RoundToInt(timeLeft).ToString();
        }
        else
        {
            // If time left is zero or less, set the text to indicate time's up
            timerText.text = "Time's Up!";
        }
    }
}
