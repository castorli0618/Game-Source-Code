using UnityEngine;

/// <summary>
/// Spawns code block obstacles in front of the bird at consistent intervals and with random variation.
/// </summary>

public class CodeBlockSpawner : MonoBehaviour 

{

    public GameObject codeBlockPrefab;   // Prefab for the code block
    public Transform birdTransform;
    public float spawnDistance = 30f;    // distance ahead of the bird to spawn blocks
    public float spawnInterval = 5f;     // horizontal spacing between blocks
    public float blockYOffset = 1f;      // optional vertical offset

    private float lastSpawnX;            // Last x position where a block was spawned
    private bool errorFlag = false;      // Flag to track if any major error occurred

    void Start()
    {
        // error checking to ensure birdTransform is assigned
        // debugs, avoid null reference
        if (birdTransform == null)
        {
            Debug.LogError("[CodeBlockSpawner] ERROR: Bird Transform not assigned!"); 
            errorFlag = true;              // Set flag to indicate initialization failure
            enabled = false;              // script disabled to prevent further errors
            return;
        }

        lastSpawnX = birdTransform.position.x;

        // Debug.Log($"[CodeBlockSpawner] Starting at X = {lastSpawnX}");
    }

    void Update()
    {
        // further processing prevented if flagged
        if (errorFlag) return;

        // furthest point we should spawn blocks to
        float targetX = birdTransform.position.x + spawnDistance;

        // keep spawning while we haven't reached the target X yet
        while (lastSpawnX < targetX)
        {
            SpawnBlock(lastSpawnX + spawnInterval); 
            lastSpawnX += spawnInterval;

            // Debug.Log($"[CodeBlockSpawner] Spawned block at X = {lastSpawnX}"); 
        }

    /// <summary>
    /// Spawns a single code block with randomized position and size.
    /// </summary>
    /// <param name="xPos">x position where the block will be placed</param>

    void SpawnBlock(float xPos)
    {
        // y position randomized near the bird’s vertical position
        float randomYPos = Random.Range(birdTransform.position.y - 3f, birdTransform.position.y + 3f);
        float randomWidth = Random.Range(1f, 4f);
        float randomHeight = Random.Range(1f, 3f); // Height and width of block

        Vector3 spawnPos = new Vector3(xPos, randomYPos, 0f);

        // Instantiate the block in the game world
        GameObject block = Instantiate(codeBlockPrefab, spawnPos, Quaternion.identity);

        if (block == null)
        {
            Debug.LogWarning("[CodeBlockSpawner] WARNING: Failed to instantiate block prefab."); 
            // ensures block is instantiated
            return;
        }

        // Set size of the block visually
        block.transform.localScale = new Vector3(randomWidth, randomHeight, 1f);

        // updates collider to match the new size
        BoxCollider2D col = block.GetComponent<BoxCollider2D>();
        if (col != null)
        {
            // reset and update collider size
            col.size = new Vector2(randomWidth, randomHeight);
            col.offset = Vector2.zero;
        }
        else
        {
            Debug.LogWarning("[CodeBlockSpawner] WARNING: Spawned block missing BoxCollider2D."); 
            // setup issues check
        }

        // Debug.Log($"[CodeBlockSpawner] Spawned Block - Pos: {spawnPos}, Size: {randomWidth}x{randomHeight}");
    }
    }
}
