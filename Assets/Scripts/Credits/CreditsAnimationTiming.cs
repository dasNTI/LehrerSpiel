using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsAnimationTiming : MonoBehaviour
{
    public int wait1;
    public int wait2;

    private Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
        StartCoroutine(routine());
        //Progress.Levels.Save(new bool[] { true, true, true });
    }

    IEnumerator routine()
    {
        bool[] l = Progress.Levels.Load();
        bool t = l[0] && l[1] && l[2];
        Debug.Log(t);

        yield return new WaitForSecondsRealtime(wait1);
        if (t) ani.SetBool("active", true);
        yield return new WaitForSecondsRealtime(wait2);
        ani.SetBool("active", false );
        yield return new WaitForSecondsRealtime(2);
        if (t) Application.Quit();
        if (!t)
        {
            changescene c = GetComponent<changescene>();
            if (c) c.change("MainMenu");
        }
    }
}