using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    [Header("Touches :")]
    [SerializeField] private Keys keys;

    [Header("Joueur : ")]
    [SerializeField] private GameObject player;

    private Vector3 offset_1;
    private Vector3 offset_2;

    [Header("Coefficients : ")]
    [SerializeField] private float movement = 0.1f;
    [SerializeField] private int limit = 5;

    void Start()
    {
        this.offset_1 = transform.position - player.transform.position;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(this.keys.Get_MOVE_CAMERA())) {

            if (Input.GetKey(this.keys.Get_UP()))
                this.offset_2 += Vector3.up * movement;

            if (Input.GetKey(this.keys.Get_LEFT()))
                this.offset_2 += Vector3.left * movement;

            if (Input.GetKey(this.keys.Get_DOWN()))
                this.offset_2 += Vector3.down * movement;

            if (Input.GetKey(this.keys.Get_RIGHT()))
                this.offset_2 += Vector3.right * movement;

        } else
            this.offset_2 = Vector3.zero;

        transform.position = player.transform.position + this.offset_1 + this.offset_2;
    }
}