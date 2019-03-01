using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsController : MonoBehaviour
{
    public GameObject[] bodyParts = new GameObject[10];

    public GameObject GetBodyPart(string name)
    {
        GameObject bodyPart = null;

        foreach (GameObject bp in this.bodyParts)
        {
            if (bp.name == name)
                bodyPart = bp;
        }

        return bodyPart;
    }
} 
