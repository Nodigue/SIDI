using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Keys keys;
    [SerializeField] private BodyPartsController bodyPartsController;

    void Update()
    {
        if ( Input.GetKeyDown(this.keys.Get_TEST()) )
        {
            bodyPartsController.head.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
        }
    }
}
