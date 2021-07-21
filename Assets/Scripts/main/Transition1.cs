using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition1 : MonoBehaviour
{
    public float frametime = 0.01f;
    public float FadeSteps = 30;
    private Image im;
    public AudioSource sound;

    public bool PlayOnStart = true;

    void Start()
    {
        im = GetComponent<Image>();
        if (PlayOnStart) StartCoroutine(transOut());
    }

    public IEnumerator transOut()
    {
        im.enabled = true;
        Color c = im.color;
        if (sound) sound.Play();
        for (int i = 1; i < FadeSteps; i++)
        {
            im.color = new Color(c.r, c.g, c.b, im.color.a - (1f / FadeSteps));
            yield return new WaitForSecondsRealtime(frametime);
            if (Time.timeScale == 0) yield return new WaitWhile(() => Time.timeScale == 0);
        }
        im.enabled = false;
    }

    public IEnumerator transIn()
    {
        im.enabled = true;
        Color c = im.color;
        im.color = new Color(c.r, c.g, c.b, 0);
        if (sound) sound.Play();
        for (int i = 0; i < FadeSteps; i++)
        {
            im.color = new Color(c.r, c.g, c.b, im.color.a + (1f / FadeSteps));
            yield return new WaitForSecondsRealtime(frametime);
            if (Time.timeScale == 0) yield return new WaitWhile(() => Time.timeScale == 0);
        }
    }
}
