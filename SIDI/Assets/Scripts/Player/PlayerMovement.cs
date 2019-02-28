using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Touches : ")]
    [SerializeField] private Keys keys;

    [SerializeField] private BodyPartsController bodyPartsController;

    public PlayerController controller;
    private float horizontalInput = 0f;

    private bool jump = false;
    private bool run = false;
    private bool crouch = false;

    private void Update()
    {
        this.horizontalInput = Input.GetAxisRaw("Horizontal") * 10f;

        if (Input.GetKeyDown("space"))
            this.jump = true;

        if (Input.GetKey("left shift"))
            this.run = true;

        if (Input.GetKey("left alt"))
            this.crouch = true;

    }

    private void FixedUpdate()
    {
        if (!Input.GetKey(this.keys.Get_MOVE_CAMERA()))
        {
            controller.Move(horizontalInput * Time.fixedDeltaTime, this.jump, this.run, this.crouch);
            reset_action();
        }

        if (Input.GetKeyDown(this.keys.Get_TEST()))
        {
        }
    }

    private void reset_action()
    {
        this.jump = false;
        this.run = false;
        this.crouch = false;
    }
}