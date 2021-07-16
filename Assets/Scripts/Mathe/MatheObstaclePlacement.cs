using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheObstaclePlacement : MonoBehaviour
{
    public int rows;
    public Sprite[] students;
    public Sprite[] bags;
    public LayerMask lm;

    public float SpawnRate = 5;

    private int LastRow;


    void Start()
    {
        StartCoroutine(routine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn()
    {
        Debug.Log("yeet");
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
        mom.lm = 11;
        mom.rows = rows;
        mom.row = row;

        o.transform.localScale = Vector3.one * 0.35f;

        PolygonCollider2D pc = o.AddComponent<PolygonCollider2D>();
    }

    IEnumerator routine()
    {
        while (true)
        {
            spawn();
            yield return new WaitForSecondsRealtime(SpawnRate + Random.RandomRange(-1, 2));
        }
    }
}
