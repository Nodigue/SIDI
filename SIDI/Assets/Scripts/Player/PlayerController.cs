using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Orientation")]
    
    [SerializeField] private bool isFacingRight = true;      //Si le personnage est tourné vers la droite

    [Header("Control")]
    
    [SerializeField] private float speed = 1f;               //Vitesse (marche)
    [SerializeField] private float runningSpeed = 2f;        //Vitesse (course)
    [SerializeField] private float crouchingSpeed = 0.5f;    //Vitesse (accroupi)

    [SerializeField] private float jumpForce = 400f;         //Puissance du saut

    [SerializeField] private bool airControl = false;        //Si le personnage peut être contrôler dans les airs

    [Header("Ground")]
    
    [SerializeField] private LayerMask groundMask;           //Masque du Sol
    [SerializeField] private Transform groundCheck;          //Point de vérification du sol
    [SerializeField] private bool isGrounded;                //Si le personnage au sol

    [Header("Collision")]
    
    [SerializeField] private float radius = 0.2f;            //Rayon du cercle de collision

    private Rigidbody2D rigidBody;                           //RigidBody2D du personnage

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //On regarde tous les colliders présent dans le cercle de collision sur le Layer du Sol
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, radius, groundMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            //Si il y en a au moins un qui n'est pas le personnage, c'est qu'il est au sol
            if (colliders[i].gameObject != this.gameObject)
            {
                isGrounded = true;
            }
        }
    }

    public void Move(float movement, bool jump)
    {
        if (isGrounded || airControl)
        {
            Vector2 velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            rigidBody.velocity = velocity;

            if (movement > 0 && !isFacingRight)
            {
               Flip();
            }
            else if (movement < 0 && isFacingRight)
            {
               Flip();
            }
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            rigidBody.AddForce(new Vector2(0f, jumpForce));
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