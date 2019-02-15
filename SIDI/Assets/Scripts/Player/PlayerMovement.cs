using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    private float horizontalInput = 0f;

    private bool jump = false;
    private bool run = false;
    private bool crouch = false;

    private void Update()
    {
        this.horizontalInput = Input.GetAxisRaw("Horizontal") * 10f;

        if (Input.GetButtonDown("Jump"))
            this.jump = true;

        if (Input.GetKey("left shift"))
            this.run = true;

        if (Input.GetKey("left alt"))
            this.crouch = true;

    }

    private void FixedUpdate()
    {
        controller.Move(horizontalInput * Time.fixedDeltaTime, this.jump, this.run, this.crouch);
        reset_action();
    }

    private void reset_action()
    {
        this.jump = false;
        this.run = false;
        this.crouch = false;
    }
}