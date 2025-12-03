# Game-Source-Code
The source code of a game I made.

using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;     // reference to the bird's Rigidbody2D component
    public float flapStrength = 5f;  
    public float moveSpeed = 2f;        

    public float groundY = -1f;         // minimum y-position, prevents the bird from falling below ground
    public float skyY = 5f;             // Mmaximum y-position, stops the bird from flying too high

    void Update()
    {
        // If the spacebar is pressed, apply upward velocity
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, flapStrength);
        }

        // Prevent bird position from falling below the ground level
        if (transform.position.y < groundY)
        {
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            myRigidbody.linearVelocity = Vector2.zero; 
            // Reset velocity when clamped
        }

        // Prevent bird position from exceeding the sky limit
        if (transform.position.y > skyY)
        {
            transform.position = new Vector3(transform.position.x, skyY, transform.position.z);
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, 0f); // Stop upward motion
        }
    }
}

