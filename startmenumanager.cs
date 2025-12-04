using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenuManager : MonoBehaviour
{
    // references UI canvases
    public GameObject startCanvas;
    public GameObject gameUICanvas;

    // UI elements on the start menu
    public GameObject titleText;
    public GameObject startButton;
    public GameObject instructionText;
    public GameObject guideText;

    // internal state tracking
    private bool instructionsVisible = false;
    private bool gameStarted = false;

    void Start()
    {
        // Pause the game at launch
        Time.timeScale = 0f;

        // show start menu, game UI hidden
        startCanvas.SetActive(true);
        gameUICanvas.SetActive(false);

        instructionText.SetActive(false); // instructions hidden
        guideText.SetActive(true);        // guide visible initially
    }

    // Called when the Start button is clicked
    public void OnStartButtonClicked()
    {
        // initial menu elements hidden
        titleText.SetActive(false);
        startButton.SetActive(false);
        guideText.SetActive(false);

        // instructions shown
        instructionText.SetActive(true);
        instructionsVisible = true;
    }

    void Update()
    {
        // Wait for any key to start the game after instructions are visible
        if (instructionsVisible && Input.anyKeyDown && !gameStarted)
        {
            // prevent start if the user is interacting with UI
            if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
                return;

            // transition to the main game UI
            startCanvas.SetActive(false);
            gameUICanvas.SetActive(true);

            // game time resumed
            Time.timeScale = 1f;

            // internal state updated
            instructionsVisible = false;
            gameStarted = true;
        }
    }
}
