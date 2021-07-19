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

    public float SpawnRate = 5;

    public bool Spawning = true;

    private int LastRow;

    private MathePlayerMovement mpm;
    private MatheFloorMovement mfm;


    void Start()
    {
        StartCoroutine(routine());
        mpm = GameObject.FindGameObjectWithTag("Player").GetComponent<MathePlayerMovement>();
        mfm = GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn()
    {
        //Debug.Log("yeet");
        int t = Random.Range(0, 2);
        int row = 0;

        GameObject o = new GameObject("obs");

        SpriteRenderer sr = o.AddComponent<SpriteRenderer>();

        switch (t)
        {
            case 0:
                int s = Random.Range(0, students.Length);
                sr.sprite = students[s];
                row = Random.Range(0, 2) * 2;
                while (row == LastRow) row = Random.Range(0, 2) * 2;
                if (row == 0) sr.flipX = true;
                break;

            case 1:
                s = Random.Range(0, bags.Length);
                sr.sprite = bags[s];
                row = Random.Range(0, 3);
                while (row == LastRow) row = Random.Range(0, 3);
                break;
        }

        LastRow = row;

        MatheObstacleMovement mom = o.AddComponent<MatheObstacleMovement>();
        mom.lm = lm;
        mom.rows = rows;
        mom.row = row;

        o.transform.localScale = Vector3.one * 0.35f;

        o.tag = "Obstacle";

        PolygonCollider2D pc = o.AddComponent<PolygonCollider2D>();
    }

    public IEnumerator routine()
    {
        yield return new WaitForSecondsRealtime(1);
        bool rep = true;
        while (Spawning)
        {
            spawn();
            yield return new WaitForSecondsRealtime(Random.RandomRange(0.5f, SpawnRate + 1.0f));
            //if (!Spawning) yield return new WaitWhile(() => !Spawning);
        }
    }
}
