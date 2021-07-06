using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathePlayerMovement : MonoBehaviour
{
    private BoxCollider2D bc;
    public LayerMask lm;
    public int rows = 2;
    public float row;
    private float y = -3.2f;
    private bool changing = false;

    private bool crash;
    public AudioSource sound;
    public AudioSource sound1;
    private SpriteRenderer sr;

    public int lives = 3;

    private Moves moves;
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        row = 0;
        moves = new Moves();
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (moves.side() != 0 && !changing && !crash)
        {
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
                GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>().speed = 0;
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
            if (crash) yield return new WaitWhile(() => crash);
        }
        changing = false;
    }

    bool TestForCrash()
    {
        float extraHeight = 0.1f;
        RaycastHit2D ray = Physics2D.Raycast(bc.bounds.center, Vector3.up, bc.bounds.extents.y + extraHeight, lm);
        Debug.DrawRay(bc.bounds.center, Vector3.up * (bc.bounds.extents.y + extraHeight));
        return ray.collider != null;
    }

    IEnumerator crashed()
    {
        float v = GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>().speed;
        GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>().speed = 0;
        yield return new WaitForSecondsRealtime(1.5f);
        GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>().speed = v;
    }

    IEnumerator crashAni()
    {
        for (int i = 0; i < 6; i++)
        {
            sound1.Play();
            sr.enabled = !sr.enabled;
            yield return new WaitForSecondsRealtime(0.25f);
        }
        crash = false;
    }
}
