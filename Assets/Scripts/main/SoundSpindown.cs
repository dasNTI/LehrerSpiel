using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpindown : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }

    public IEnumerator Spindown()
    {
        GetComponent<Animation>().Play();
        yield return new WaitForSecondsRealtime(2);
        audio.Stop();
    }

    public void Restart()
    {
        audio.pitch = 1;
        audio.Play();
    }
}
