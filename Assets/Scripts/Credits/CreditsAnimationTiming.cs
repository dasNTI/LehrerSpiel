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
    }

    IEnumerator routine()
    {
        yield return new WaitForSecondsRealtime(wait1);
        ani.SetBool("active", true);
        yield return new WaitForSecondsRealtime(wait2);
        ani.SetBool("active", false );
    }
}
