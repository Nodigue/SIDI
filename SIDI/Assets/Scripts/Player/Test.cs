using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Keys keys;
    [SerializeField] private BodyPartsController bp;



    void FixedUpdate()
    {
        if (Input.GetKeyDown(this.keys.Get_TEST()))
        {
        }
    }
}
