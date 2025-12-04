using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tracks the bird's simulated progress, updates the score bar.
/// Win condition triggered and win screen shown when distance reached
/// </summary>

public class ScoreManager : MonoBehaviour
{
    public Transform bird;
    public Slider scoreBar;             // UI slider showing travel progress
    public float winDistance = 1000f;   // distance required to win game

    private bool hasWon = false;        // prevent multiple win triggers

    void Start()
    {
        // error checking, ensure bird assigned
        if (bird == null)
            Debug.LogError("[ScoreManager] Bird reference is missing!");
        else
            Debug.Log("[ScoreManager] Bird assigned: " + bird.name); // trace log

        // ensure scorebar assigned
        if (scoreBar == null)
            Debug.LogError("[ScoreManager] ScoreBar reference is missing!");
        else
            Debug.Log("[ScoreManager] ScoreBar assigned: " + scoreBar.name);
    }

    void Update()
    {
        // prevent further updates after win or if UI reference is missing
        if (hasWon || scoreBar == null) return;

        // Simulate the bird moving forward over time 
        float simulatedDistance = Mathf.Min(Time.time * 5f, winDistance);

        // Update the slider value based on percentage of distance travelled
        scoreBar.value = simulatedDistance / winDistance;

        // Debug output for tracking slider logic during testing
        Debug.Log($"[ScoreManager] Simulated distance: {simulatedDistance}, Slider value: {scoreBar.value}");

        // Check win condition
        if (simulatedDistance >= winDistance)
        {
            hasWon = true; // lock to prevent repeat execution
            Time.timeScale = 0f; // pause the game

            // ensures win condition triggered
            Debug.Log("[ScoreManager] You win! Triggering win screen.");

            // show the win screen
            if (WinScreenUI.Instance != null)
            {
                WinScreenUI.Instance.ShowWinScreen();
            }
            else
            {
                Debug.LogError("[ScoreManager] WinScreenUI.Instance is null!");
            }
        }
    }
}
