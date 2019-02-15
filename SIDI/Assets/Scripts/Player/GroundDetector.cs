using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isGrounded;
    private Collider2D groundTrigger;

    private void Awake()
    {
        groundTrigger = GetComponent<Collider2D> ();    
    }

    void Update()
    {
        print("isGrounded = " + isGrounded);
    }

    void OnTriggerEnter()
    {
        isGrounded = true;
    }

    void OnTriggerStay()
    {
        isGrounded = true;
    }

    void OnTriggerExit()
    {
        isGrounded = false;
    }
}
