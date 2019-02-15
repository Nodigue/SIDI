using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Orientation")]
    
    [SerializeField] private bool isFacingRight = true;      //Si le personnage est tourné vers la droite

    [Header("Control")]
    
    [SerializeField] private float walkingSpeed = 1f;        //Vitesse (marche)
    [SerializeField] private float runningSpeed = 2f;        //Vitesse (course)
    [SerializeField] private float crouchingSpeed = 0.5f;    //Vitesse (accroupi)

    [SerializeField] private float jumpForce = 400f;         //Puissance du saut

    [Header("Ground")]
    
    [SerializeField] private Transform ground;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool isGrounded = false;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        this.isGrounded = checkGround();
    }

    private bool checkGround()
    {
        bool isGrounded = false;

        BoxCollider2D groundCheckCollider = this.groundCheck.GetComponent<BoxCollider2D>();

        if (groundCheckCollider.IsTouching(ground.GetComponent<BoxCollider2D>()))
            isGrounded = true;

        return isGrounded;
    }

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

        Vector2 flip = transform.localScale;
        flip.x *= -1;
        transform.localScale = flip;
    }
}