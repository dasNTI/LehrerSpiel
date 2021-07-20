using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pausing
{
    public static bool Pausable = true;
    public static bool Paused = false;

    public static void setPaused(bool p)
    {
        if (p)
        {
            if (Pausable)
            {
                Paused = p;
                Time.timeScale = 0;
            }
        }else
        {
            Paused = p;
            Time.timeScale = 1;
        }
    }
}
