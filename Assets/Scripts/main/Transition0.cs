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

    void Start()
    {
        im = GetComponent<Image>();
        StartCoroutine(transOut());
    }

    public IEnumerator transOut()
    {
        sound.Play();
        for (int i = 0; i < sprites.Length; i++)
        {
            im.sprite = sprites[i];
            yield return new WaitForSecondsRealtime(frametime);
        }
    }

    public IEnumerator transIn()
    {
        
        sound.Play();
        for (int i = sprites.Length; i > 0; i++)
        {
            im.sprite = sprites[i];
            yield return new WaitForSecondsRealtime(frametime);
        }
    }
}
