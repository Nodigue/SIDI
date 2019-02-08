using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 300f;

    private Rigidbody2D rb2d;

    public Transform groundCheck;
    private bool grounded = false;

    private bool jumping = false;
    
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        if (!jumping)
            rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb2d.velocity.y);

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
            rb2d.AddForce(Vector2.up * jumpPower);
        }
    }
}