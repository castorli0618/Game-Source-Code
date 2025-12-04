using UnityEngine;

/// <summary>
/// Spawns a wide, continuous ground beneath the game world.
/// Designed to provide an illusion of infinite terrain using a single large prefab.
/// </summary>

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;           // ground prefab assign
    public float groundHeight = -3f;          // y-axis position for placing the ground
    public float infiniteWidth = 10000f;      // width of the tiled ground

    void Start()
    {
        // Error check, ensure prefab assigned in the Inspector
        if (groundPrefab == null)
        {
            Debug.LogError("[GroundSpawner] ERROR: GroundPrefab not assigned!"); // critical error debug
            enabled = false;     // disable script to avoid further errors
            return;
        }

        // Debug.Log("[GroundSpawner] Starting ground generation...");

        // begin ground generation at runtime
        SpawnInfiniteGround();
    }

    /// <summary>
    /// Instantiates a single wide ground object and configures its sprite to tile across.
    /// </summary>
    
    void SpawnInfiniteGround()
    {
        Vector3 spawnPosition = new Vector3(0, groundHeight, 0); // place the ground at specified height
        GameObject ground = Instantiate(groundPrefab, spawnPosition, Quaternion.identity);

        if (ground == null)
        {
            Debug.LogWarning("[GroundSpawner] WARNING: Ground instantiation failed."); 
            return;
        }

        // access the SpriteRenderer for tiling configuration
        SpriteRenderer sr = ground.GetComponent<SpriteRenderer>();

        if (sr != null && sr.drawMode == SpriteDrawMode.Tiled)
        {
            // set the width without stretching
            sr.size = new Vector2(infiniteWidth, sr.size.y);

            // confirm tiling
            Debug.Log("[GroundSpawner] Spawned infinite tiled ground.");
        }
        else
        {
            Debug.LogWarning("[GroundSpawner] WARNING: SpriteRenderer not in Tiled mode.");
            Debug.LogWarning("Make sure the ground prefab has 'Draw Mode: Tiled' enabled in the SpriteRenderer.");
        }

        // Debug.Log($"[GroundSpawner] Ground placed at {spawnPosition}, width = {infiniteWidth}");
    }
}
