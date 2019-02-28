using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsController : MonoBehaviour
{
    public GameObject[] body_parts = new GameObject[10];

    public GameObject GetBodyPart(string name)
    {
        GameObject body_part = null;

        foreach (GameObject bp in this.body_parts)
        {
           if (bp.name == name)
                body_part = bp;
        }
        
        return body_part;
    }
} 
