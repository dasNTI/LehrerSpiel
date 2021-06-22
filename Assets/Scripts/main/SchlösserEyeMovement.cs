using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchlösserEyeMovement : MonoBehaviour
{
    public Sprite[] left;
    public Sprite[] middle;
    public Sprite[] right;

    public int dir = 0;
    private int dir2;
    private float check;
    private bool blinke = false;
    private bool closed = false;

    private Moves moves;

    private GameObject p;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        p = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(update1());
        //StartCoroutine(update2());
        moves = new Moves();
    }

    void Update()
    {
        dir2 = dir;
        if (dir2 != check && !blinke && !closed)
        {
            setDir(dir2);
            check = dir2;
        }

        float r = p.GetComponent<Rigidbody2D>().velocity.y;
        if (dir2 == 1)
        {
            //Debug.Log(r);
            transform.eulerAngles = new Vector3(0, 0, (r / 2) * moves.side().CompareTo(0));
        }else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void close(bool t)
    {
        closed = t;
        if (t)
        {
            sr.sprite = middle[5];
        }else
        {
            setDir(dir2);
        }
    }

    IEnumerator update1()
    {
        while (true)
        {
            if (!closed) StartCoroutine(blink());
            yield return new WaitForSecondsRealtime(6 + Random.Range(-3, 3));
        }
    }

    IEnumerator update2()
    {
        while (true) {
            setDir(Random.Range(-1, 1));
            yield return new WaitForSecondsRealtime(6 + Random.Range(-3, 3));
        }
    }

    IEnumerator blink()
    {
        float dur = 0.05f;
        blinke = true;
        switch (dir2)
        {
            case -1:
                for (int i = 0; i < 7; i++)
                {
                    sr.sprite = left[i];
                    yield return new WaitForSecondsRealtime(dur);
                }
            break;

            case 0:
                for (int i = 0; i < 7; i++)
                {
                    sr.sprite = middle[i];
                    yield return new WaitForSecondsRealtime(dur);
                }
               break;

            case 1:
                for (int i = 0; i < 7; i++)
                {
                    sr.sprite = right[i];
                    yield return new WaitForSecondsRealtime(dur);
                }
            break;
        }
        blinke = false;
    }

    void setDir(float i)
    {
        if (!blinke)
        {
            switch (i)
            {
                case -1:
                    sr.sprite = left[0];
                    break;
                case 0:
                    sr.sprite = middle[0];
                    break;
                case 1:
                    sr.sprite = right[0];
                    break;
            }
        }
    }
}
