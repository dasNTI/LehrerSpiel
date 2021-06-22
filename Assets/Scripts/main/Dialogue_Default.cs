using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Default : MonoBehaviour
{
    public string text = "test";
    public bool side = false;
    public Sprite[] sprites;
    private SpriteRenderer sr;
    public AudioSource popup;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    public void show(string t)
    {
        text = t;   
        if (!side)
        {
            sr.sprite = sprites[0];
        }else
        {
            sr.sprite = sprites[1];
        }
        sr.enabled = true;
        popup.Play();
    }
}
