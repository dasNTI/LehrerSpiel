using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition0 : MonoBehaviour
{
    public Sprite[] sprites;
    public float frametime = 0.2f;
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
        sound.Play();
        for (int i = 0; i < sprites.Length; i++)
        {
            im.sprite = sprites[i];
            yield return new WaitForSecondsRealtime(frametime);
        }
        im.enabled = false;
    }

    public IEnumerator transIn()
    {
        im.enabled = true;      
        sound.Play();
        for (int i = sprites.Length; i > 0; i++)
        {
            im.sprite = sprites[i];
            yield return new WaitForSecondsRealtime(frametime);
        }
    }
}
