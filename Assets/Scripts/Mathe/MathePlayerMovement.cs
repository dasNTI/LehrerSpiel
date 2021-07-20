﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathePlayerMovement : MonoBehaviour
{
    private BoxCollider2D bc;
    public LayerMask lm;
    public int rows = 2;
    public float row = 1;
    private float y = -3.2f;
    private bool changing = false;
    private float InitSpeed;

    private bool crash = false;
    public AudioSource sound;
    public AudioSource sound1;
    private SpriteRenderer sr;

    public int lives = 3;

    public Sprite[] sprites;
    private bool walks = false;

    private Moves moves;
    private MatheFloorMovement mfm;
    private MatheObstaclePlacement mop;
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        row = 0;
        moves = new Moves();
        lives = 3;
        mfm = GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>();
        mop = GameObject.Find("Schoop").GetComponent<MatheObstaclePlacement>();
        InitSpeed = mfm.speed;
        StartCoroutine(walk()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (moves.side() != 0 && !changing && !crash)
        {
            //Debug.Log(moves.side());
            switch (moves.side().CompareTo(0))
            {
                case -1: 
                    if (Mathf.Round(row) > 0) StartCoroutine(change(-1));
                    break;

                case 1:
                    if (Mathf.Round(row) < rows - 1) StartCoroutine(change(1));
                    break;
            }
        }

        if (TestForCrash() && !crash)
        {
            crash = true;
            lives--;
            sound.Play();
            StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MatheCameraMovement>().shake());
            if (lives == 0)
            {
                GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>().dir = 0.1f;
                GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>().speed = 0;
                CrashOverlayAni1();
                GameObject.Find("Schoop").GetComponent<MatheObstaclePlacement>().Spawning = false;
                GameObject.Find("Deathtext").GetComponent<DeathtextChoose>().choose();
                StartCoroutine(GameObject.Find("Music").GetComponent<SoundSpindown>().Spindown());
            }else
            {
                StartCoroutine(crashed());
                StartCoroutine(crashAni());
            }
        }
            
        if (!crash) transform.position = new Vector3((row * 2 + 1) * 11 / rows / 2 - 5.5f, y, 0);
        if (crash && lives == 0) transform.Translate(new Vector3(0, -GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>().dir, 0));
    }

    IEnumerator change(float r)
    {
        changing = true;
        float dur = 0.1f;
        float steps = 20;
        for (int i = 0; i < steps; i++)
        {
            float c = 1 / steps;
            row += c * r;
            yield return new WaitForSeconds(dur / steps);
            if (crash && lives == 3) i = (int) steps + 1;
            if (crash) yield return new WaitWhile(() => crash);
            if (Time.timeScale == 0) yield return new WaitWhile(() => Time.timeScale == 0);
        }
        changing = false;
    }

    bool TestForCrash()
    {
        float extraHeight = 0.1f;
        RaycastHit2D ray = Physics2D.Raycast(bc.bounds.center, Vector3.up, bc.bounds.extents.y + extraHeight, lm);
        Debug.DrawRay(bc.bounds.center + Vector3.down * bc.bounds.extents.y, Vector3.up * (bc.bounds.extents.y + extraHeight));
        return ray.collider != null;
    }

    IEnumerator crashed()
    {
        mop.Pause = true;
        float v = GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>().speed;
        GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>().speed = 0;
        yield return new WaitForSecondsRealtime(1.5f);
        if (Time.timeScale == 0) yield return new WaitWhile(() => Time.timeScale == 0);
        GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>().speed = v;
        mop.Pause = false;
    }

    IEnumerator crashAni()
    {
        for (int i = 0; i < 6; i++)
        {
            sound1.Play();
            sr.enabled = !sr.enabled;
            yield return new WaitForSecondsRealtime(0.25f);
            if (Time.timeScale == 0) yield return new WaitWhile(() => Time.timeScale == 0);
        }
        crash = false;
    }

    IEnumerator walk()
    {
        int i = 0;
        float dur = 0.3f;
        while (true)
        {
            sr.sprite = sprites[i];
            i = walks ? 0 : 1;
            walks = !walks;
            yield return new WaitForSecondsRealtime(dur);
            if (crash) yield return new WaitWhile(() => crash);
            if (Time.timeScale == 0) yield return new WaitWhile(() => Time.timeScale == 0);
        }
    }

    public void CrashOverlayAni1()
    {
        GameObject.FindGameObjectWithTag("Overlay1").GetComponent<Animator>().SetBool("active", true);
        GameObject.Find("Deathscreen").GetComponent<Animator>().SetBool("active", true);
    }

    public void CrashOverlayAni2()
    {
        GameObject.FindGameObjectWithTag("Overlay1").GetComponent<Animator>().SetBool("active", false);
        GameObject.Find("Deathscreen").GetComponent<Animator>().SetBool("active", false);
    }

    public void respawn()
    {
        StopAllCoroutines();
        crash = false;
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Obstacle")) Destroy(i);
        lives = 3;
        GetComponent<MatheWayCounter>().Current = 0;
        GameObject.Find("Schoop").GetComponent<MatheObstaclePlacement>().Spawning = true;
        StartCoroutine(GameObject.Find("Schoop").GetComponent<MatheObstaclePlacement>().routine());
        mfm.speed = InitSpeed;
        GameObject.Find("Music").GetComponent<SoundSpindown>().Restart();
        CrashOverlayAni2();
        StartCoroutine(walk());
        changing = false;
        row = 1;
    }
}   
