using UnityEngine;

/// <summary>
/// The code challenge block in the game, triggers a coding question when the player collides with it
/// Block destroys itself when off-screen or when question is answered.
/// </summary>

public class CodeBlock : MonoBehaviour
{
    public float moveSpeed = 2f;              // speed at which the block moves left
    public float destroyOffset = 20f;         // how far off-screen before it's destroyed

    public float minWidth = 1f;               
    public float maxWidth = 2.5f;             // block maximum and minimum width

    public float fixedHeight = 1f;            // Fixed block height
    public float minY = -1f;                  
    public float maxY = 3.5f;                 // maximum and minimum spawn y position

    private Transform bird;                   // reference to the player's transform
    private bool hasTriggered = false;        // flag, ensure question is only triggered once

    public QuestionManager questionManager;   // reference to the question manager script

    void Start()
    {
        // find player by tag
        GameObject birdObj = GameObject.FindWithTag("Player");
        if (birdObj != null)
            bird = birdObj.transform;
        else
            Debug.LogWarning("[CodeBlock] Player object not found!"); 
            // debug trace for null reference, ensures bird is connected

        // set a random width for the code block
        float width = Random.Range(minWidth, maxWidth);
        transform.localScale = new Vector3(width, fixedHeight, 1f);

        // set a random vertical position within a range
        float randomY = Random.Range(minY, maxY);
        transform.position = new Vector3(transform.position.x, randomY, transform.position.z);

        // ensure the collider behaves as a trigger
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
            col.isTrigger = true;
        else
            Debug.LogWarning("[CodeBlock] No BoxCollider2D attached!"); 
            // debug if no collider
    }

    void Update()
    {
        // only move if not already triggered
        if (!hasTriggered)
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // if bird exists and block is far behind it, destroy the block
        if (bird != null && transform.position.x < bird.position.x - destroyOffset)
        {
            // Debug.Log("[CodeBlock] Block destroyed (off screen).");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ensure only triggers once and only with player
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;

        if (questionManager != null)
        {
            Time.timeScale = 0f; // pause the game for the question
            questionManager.ShowNextQuestion(OnQuestionAnswered); // show question and wait for answer
            // Debug.Log("[CodeBlock] Question triggered.");
        }
        else
        {
            Debug.LogWarning("[CodeBlock] QuestionManager reference not set!"); 
            // debugging, ensures no problems with questionmanager
        }
    }

    /// <summary>
    /// Callback invoked when the question is answered correctly.
    /// Resumes gameplay and removes the code block.
    /// </summary>
    
    void OnQuestionAnswered()
    {
        Time.timeScale = 1f;   // game is resumed
        Destroy(gameObject);
        // Debug.Log("[CodeBlock] Question answered. Block removed.");
    }
}
