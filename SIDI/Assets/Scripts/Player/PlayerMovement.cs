using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    private float horizontalInput = 0f;

    private bool jump = false;

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * 10f;

        if (Input.GetButtonDown("Jump"))
            jump = true;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalInput * Time.fixedDeltaTime, jump);
        jump = false;
    }
}