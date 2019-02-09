using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 300f;

    private Rigidbody2D rb;

    private LayerMask groundLayerMask;
    private float lastDistance;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundLayerMask = LayerMask.GetMask("Ground");
    }
    
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
    }
}
    // Check if the user is attempting to jump
    float jumpAxis = Input.GetAxisRaw("Jump");
    if (jumpAxis != 0 && rb.velocity.y <= 0)
    {
        // Raycast from the feet of the player directly down (or the origin, doesn't matter)
        RaycastHit2D hit2D = Physics2D.Raycast(rb.position - new Vector2(0f, 0.5f), Vector2.down, 0.2f, environmentLayerMask);
        // If the raycast hit something
        if (hit2D)
        {
            // Check if the distance of the object hit is less than the last distance checked
            if (hit2D.distance < lastDistance)
            {
                // Update the last distance if the object below is less than the last known distance
                lastDistance = hit2D.distance;
            }
            else
            {
                // If the hit distance is not less than the lass distance, then jump (he isn't going to go any lower)
                lastDistance = 100f;
                Jump(jumpAxis * jumpForce * Time.deltaTime);
            }
        }
    }
}
void Jump(float force)
{
    if (force < 0)
    {
        return;
    }
    rb.AddForce(new Vector2(0, force));
}