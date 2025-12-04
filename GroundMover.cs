using UnityEngine;

public class GroundMover : MonoBehaviour
{
    public float moveSpeed = 2f;      // speed at which the ground moves left
    public float destroyX = -20f;     // x-position at which the ground object gets destroyed

    void Update()
    {
        // move the ground left each frame based on moveSpeed
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // destroy the ground object once it moves past a point
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
