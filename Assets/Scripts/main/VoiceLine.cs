using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLine : MonoBehaviour
{
    public AudioClip vl; 
    public float dur = 0.7f;
    void Start()
    {
        play();
    }

    public void play()
    {
        StartCoroutine(p());
    }
    
    IEnumerator p()
    {
        StartCoroutine(fade(0));
        GameObject vc = new GameObject();
        AudioSource ao = vc.AddComponent<AudioSource>();
        ao.clip = vl;
        yield return new WaitForSecondsRealtime(dur);
        ao.Play();
        yield return new WaitForSecondsRealtime(vl.length);
        StartCoroutine(fade(1));
    }

    IEnumerator fade(float n)
    {
        /*float t = Time.timeScale;
        for (int i = 1; i < 100; i++)
        {
            Debug.Log(Time.timeScale);
            Time.timeScale = t + (n - t) / 100 * i;
            yield return new WaitForSecondsRealtime(1 / dur);
        }*/

        Time.timeScale = n;
        yield return null;
    }
}
