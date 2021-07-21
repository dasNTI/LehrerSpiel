using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheGoalReached : MonoBehaviour
{
    private MatheWayCounter mwc;
    private MatheObstaclePlacement mop;
    private MathePlayerMovement mpm;

    public AudioSource Music;
    public AudioSource End;

    bool done = false;

    void Start()
    {
        mwc = GetComponent<MatheWayCounter>();
        mop = GameObject.Find("Schoop").GetComponent<MatheObstaclePlacement>();
        mpm = GameObject.FindGameObjectWithTag("Player").GetComponent<MathePlayerMovement>();
    }

    private void Update()
    {
        if (mwc.Current > mwc.FinishLine && !done)
        {
            done = true;
            wait();
        }

        if (mwc.Current < mwc.FinishLine & done) done = false;
    }

    void wait()
    {
        mop.Spawning = false;
        StartCoroutine(fade());
        mpm.finish();
        End.Play();
    }

    IEnumerator fade()
    {
        float v = Music.volume;
        float dur = 0f;
        int steps = 20;
        for (int i = 0; i < steps; i++)
        {
            Music.volume = v - (v / steps);
            yield return new WaitForSecondsRealtime(dur / steps);
        }

        Music.Stop();
    }
}
