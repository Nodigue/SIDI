using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BodyPartsController bodyPartsController;

    [Header("Orientation")]
    
    [SerializeField] private bool isFacingRight = true;

    [Header("Control")]
    
    [SerializeField] private float walkingSpeed = 1f;
    [SerializeField] private float runningSpeed = 2f;
    [SerializeField] private float crouchingSpeed = 0.5f;

    [SerializeField] private float jumpForce = 400f;
    //[Header("Ground")]
    
    //[SerializeField] private Transform ground;
    //[SerializeField] private Transform groundCheck;
    private bool isGrounded = true;

    private Rigidbody2D rigidBody;

    private void Awake()    
    {
        rigidBody = bodyPartsController.body.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //this.isGrounded = checkGround();
    }

    /*private bool checkGround()
    {
        bool isGrounded = false;

        BoxCollider2D groundCheckCollider = this.groundCheck.GetComponent<BoxCollider2D>();

        if (groundCheckCollider.IsTouching(ground.GetComponent<BoxCollider2D>()))
            isGrounded = true;

        return isGrounded;
    }*/

    public void Move(float movement, bool jump, bool run, bool crouch)
    {
        float speed = this.walkingSpeed;

        if (this.isGrounded)
        {
            if (run)
                speed = this.runningSpeed;
            else if (crouch)
                speed = this.crouchingSpeed;

            Vector2 velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            rigidBody.velocity = velocity;

            if (jump)
            {
                rigidBody.AddForce(new Vector2(0f, jumpForce));
            }

            if (movement > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (movement < 0 && isFacingRight)
            {
                Flip();
            }

        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
    }
}