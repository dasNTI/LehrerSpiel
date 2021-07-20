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
        Color c = im.color;
        if (sound) sound.Play();
        for (int i = 1; i < FadeSteps; i++)
        {
            im.color = new Color(c.r, c.g, c.b, im.color.a - (1 / FadeSteps));
            yield return new WaitForSecondsRealtime(frametime);
        }
        im.enabled = false;
    }

    public IEnumerator transIn()
    {
        im.enabled = true;
        Color c = im.color;
        if (sound) sound.Play();
        for (int i = 0; i < FadeSteps; i++)
        {
            im.color = new Color(c.r, c.g, c.b, 255 / FadeSteps * i);
            yield return new WaitForSecondsRealtime(frametime);
        }
    }
}
