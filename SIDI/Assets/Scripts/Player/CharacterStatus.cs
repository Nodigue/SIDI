using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    private bool is_idle;

    void Awake()
    {
        this.is_idle = true;    
    }

    public bool IsIdle()
    {
        return this.is_idle;
    }
}
