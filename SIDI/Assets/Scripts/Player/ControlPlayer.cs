using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlPlayer : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float speedJump;

    private Vector2 movement;

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 speed = new Vector2(speedX, speedY);

        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        if (Input.GetKey(KeyCode.Space))
        {
            //jump
        }

    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}


