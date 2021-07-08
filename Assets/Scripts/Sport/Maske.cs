using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Drawing;

public class Maske : MonoBehaviour
{
    float check = 0;
    public bool on = false;

    private SpriteRenderer sr;

    public LayerMask lm;
    private BoxCollider2D bc;

    private Moves moves;

    public VolumeProfile vp;

    bool alerting = false;

    public AudioSource Alert;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        moves = new Moves();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Ability1") != check)
        {
            check = Input.GetAxis("Ability1");
            if (check == 1) changeOn();
        }

        sr.enabled = on;
    }

    public void changeOn()
    {
        if (!on)
        {
            on = true;
        }
        else if (on)
        {
            on = false;
        }
    }

    bool CheckAbilityAvailable()
    {
        int len = 10;
        int rays = 20;
        float mul = 360 / rays;
        bool o = false;
        for (int i = 0; i <= rays; i++)
        {
            RaycastHit2D ray = Physics2D.Raycast(bc.bounds.center, new Vector2(Mathf.Cos(i * mul), Mathf.Sin(i * mul)), len, lm);
            Debug.DrawRay(bc.bounds.center, new Vector2(Mathf.Cos(i * mul), Mathf.Sin(i * mul)) * len);
            if (ray.collider != null) o = true;
        }
        return o;
    }

    public void emit()
    {
        if (!alerting && !on)
        {
            alerting = true;
            Alert.Play();
            StartCoroutine(OverlayAni1());
            StartCoroutine(OverlayAni2());
        }
    }

    IEnumerator OverlayAni1()
    {
        yield return null;
    }

    IEnumerator OverlayAni2()
    {
        yield return null;
    }
}
