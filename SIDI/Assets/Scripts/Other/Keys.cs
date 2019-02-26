using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    private readonly string UP = "z";
    private readonly string LEFT = "q";
    private readonly string DOWN = "s";
    private readonly string RIGHT = "d";

    private readonly string MOVE_CAMERA = "p";

    public string Get_UP()
    {
        return this.UP;
    }
    public string Get_LEFT()
    {
        return this.LEFT;
    }
    public string Get_DOWN()
    {
        return this.DOWN;
    }
    public string Get_RIGHT()
    {
        return this.RIGHT;
    }

    public string Get_MOVE_CAMERA()
    {
        return this.MOVE_CAMERA;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
