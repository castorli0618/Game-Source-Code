using UnityEngine;

public class WinScreenUI : MonoBehaviour
{
    public static WinScreenUI Instance;

    // UI panel that will be shown when the player wins
    public GameObject winPanel;

    void Awake()
    {
        // prevent multiple instances
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // ensure the win panel is hidden at the start of the game
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    /// <summary>
    /// Activates the win screen UI.
    /// </summary>
    
    public void ShowWinScreen()
    {
        if (winPanel != null)
        {
            // show the win panel, pause game
            winPanel.SetActive(true);
            Time.timeScale = 0f; // stops all movement and action

            Debug.Log("You Win screen displayed.");
        }
    }
}
