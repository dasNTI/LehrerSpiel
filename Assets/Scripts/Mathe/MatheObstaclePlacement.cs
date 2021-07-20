using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheObstaclePlacement : MonoBehaviour
{
    public int rows;
    public Sprite[] students;
    public Sprite[] bags;
    public Sprite[] Stuff;
    public LayerMask lm;

    private float SpawnRate = 5;
    public float[] SpawnDifficulties;
    private float prevSpawnRate;

    private float FloorSpeed;
    public float[] FloorSpeeds;

    public bool Spawning = true;
    public bool Pause = false;

    private int LastRow;

    private MathePlayerMovement mpm;
    private MatheFloorMovement mfm;
    private MatheWayCounter mwc;


    void Start()
    {
        StartCoroutine(routine());
        mpm = GameObject.FindGameObjectWithTag("Player").GetComponent<MathePlayerMovement>();
        mfm = GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>();
        mwc = GameObject.FindGameObjectWithTag("Player").GetComponent<MatheWayCounter>();
        SpawnRate = SpawnDifficulties[0];
        prevSpawnRate = SpawnRate;
        FloorSpeed = FloorSpeeds[0];
        mfm.speed = FloorSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawning && !Pause)
        {
            float w = mwc.Current / mwc.FinishLine * 3;
            SpawnRate = SpawnDifficulties[(int)Mathf.Floor(w)];
            if (SpawnRate != prevSpawnRate)
            {
                prevSpawnRate = SpawnRate;
                AlertHarder();
            }

            if (FloorSpeed != FloorSpeeds[(int)Mathf.Floor(w)])
            {
                FloorSpeed = FloorSpeeds[(int)Mathf.Floor(w)];
                mfm.speed = FloorSpeed;
            }
        }
    }

    void spawn()
    {
        //Debug.Log("yeet");
        int t = Random.Range(0, 4);
        int row = 0;

        GameObject o = new GameObject("obs");

        bool del = false;

        SpriteRenderer sr = o.AddComponent<SpriteRenderer>();

        switch (t)
        {
            case 0:
                int s = Random.Range(0, students.Length);
                sr.sprite = students[s];
                row = Random.Range(0, 2) * 2;
                while (row == LastRow) row = Random.Range(0, 2) * 2;
                if (row == 0) sr.flipX = true;
                o.transform.localScale = Vector3.one * 0.35f;
                break;

            case 1:
                s = Random.Range(0, bags.Length);
                sr.sprite = bags[s];
                row = Random.Range(0, 3);
                while (row == LastRow) row = Random.Range(0, 3);
                //o.transform.localScale = Vector3.one * 0.35f;
                break;

            case 2:
                s = Random.Range(0, Stuff.Length);
                sr.sprite = Stuff[s];
                row = Random.Range(0, 3);
                while (row == LastRow) row = Random.Range(0, 3);
                break;

            case 3:
                Sprite[] b = new Sprite[bags.Length + Stuff.Length];
                bags.CopyTo(b, 0);
                Stuff.CopyTo(b, bags.Length);

                s = Random.Range(0, b.Length);
                sr.sprite = b[s];
                row = Random.Range(0, 3);
                if (row != 1) del = true;
                break;
        }

        LastRow = row;

        MatheObstacleMovement mom = o.AddComponent<MatheObstacleMovement>();
        mom.lm = lm;
        mom.rows = rows;
        mom.row = row;

        o.tag = "Obstacle";

        PolygonCollider2D pc = o.AddComponent<PolygonCollider2D>();

        if (del) Destroy(o);
    }

    public IEnumerator routine()
    {
        yield return new WaitForSecondsRealtime(1);
        bool rep = true;
        while (Spawning)
        {
            spawn();
            yield return new WaitForSecondsRealtime(Random.RandomRange(0.5f, SpawnRate + 1.0f));
            if (Pause) yield return new WaitWhile(() => Pause);
        }
    }

    void AlertHarder()
    {
        GameObject.Find("Schneller").GetComponent<Animation>().Play();
        GameObject.Find("SpeedUp").GetComponent<AudioSource>().Play();
    }
}
