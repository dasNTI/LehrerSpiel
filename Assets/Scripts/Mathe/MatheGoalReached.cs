using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheGoalReached : MonoBehaviour
{
    private MatheWayCounter mwc;
    private MatheObstaclePlacement mop;

    public AudioSource Music;
    public AudioSource End;
    void Start()
    {
        mwc = GetComponent<MatheWayCounter>();
        mop = GameObject.Find("Schoop").GetComponent<MatheObstaclePlacement>();
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitUntil(() => mwc.Current >= mwc.FinishLine);
        mop.Spawning = false;
        StartCoroutine(fade());
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
