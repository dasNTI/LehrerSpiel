using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves
{
    public string mode = "keyboard";
    public int sid = 0;
    public bool jum = false;

    public float side()
    {
        if (mode == "gamepad")
        {
            return 1;
        }
        else if (mode == "keyboard")
        {
            return Input.GetAxis("Horizontal");
        }else
        {
            return sid;
        }
    }

    public bool jump()
    {
        if (mode == "gamepad")
        {
            return true;
        }else if (mode == "keyboard")
        {
            return Input.GetAxis("Jump") == 1;
        }else
        {
            return jum;
        }
    }
}