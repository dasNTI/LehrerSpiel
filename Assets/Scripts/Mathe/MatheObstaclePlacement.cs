using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheObstaclePlacement : MonoBehaviour
{
    public int rows;
    public Sprite[] students;


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
        int t = 0; // Random.Range(0, stuff.Length - 1);
        int row = 0;

        GameObject o = new GameObject("obs");
        o.layer = 11;

        SpriteRenderer sr = o.AddComponent<SpriteRenderer>();

        switch (t)
        {
            case 0:
                int s = Random.Range(0, students.Length);
                sr.sprite = students[s];
                row = Random.Range(0, 2) * 2;
                if (row == 0) sr.flipX = true;
                break;

        }

        MatheObstacleMovement mom = o.AddComponent<MatheObstacleMovement>();
        mom.rows = rows;
        mom.row = row;

        o.transform.localScale = Vector3.one * 0.35f;

        PolygonCollider2D pc = o.AddComponent<PolygonCollider2D>();
    }

    IEnumerator routine()
    {
        for (int i = 0; i < 10; i++)
        {
            spawn();
            yield return new WaitForSecondsRealtime(5);
        }
    }
}
