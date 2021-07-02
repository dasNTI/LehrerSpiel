using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Maske : MonoBehaviour
{
    float check = 0;
    bool on = true;

    private SpriteRenderer sr;

    public LayerMask lm;
    private BoxCollider2D bc;

    public VolumeProfile vp;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mask") != check)
        {
            check = Input.GetAxis("Mask");
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
        else if (CheckAbilityAvailable() && on)
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
            if (ray.collider == null) o = true;
        }
        return o;
    }
}
