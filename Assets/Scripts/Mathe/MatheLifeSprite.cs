using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatheLifeSprite : MonoBehaviour
{
    public int Life;
    private int lives;
    private bool alive = true;
    private Image im;

    void Start()
    {
        im = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        lives = GameObject.FindGameObjectWithTag("Player").GetComponent<MathePlayerMovement>().lives;

        if (alive && lives < Life)
        {
            StartCoroutine(die());
            alive = false;
        }

        if (!alive && lives >= Life)
        {
            alive = true;
            im.enabled = true;
        }
    }

    IEnumerator die()
    {
        for (int i = 0; i < 7; i++)
        {
            im.enabled = !im.enabled;
            yield return new WaitForSecondsRealtime(0.25f);
            if (Time.timeScale == 0) yield return new WaitWhile(() => Time.timeScale == 0);
        }
    }
}
