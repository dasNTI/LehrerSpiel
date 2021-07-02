using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheObstaclePlacement : MonoBehaviour
{
    public int rows;
    public Sprite[][] stuff;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn()
    {
        int t = 0; // Random.Range(0, stuff.Length - 1);
        int row = 0;

        GameObject o = new GameObject("obs");
        o.layer = 11;

        SpriteRenderer sr = o.AddComponent<SpriteRenderer>();

        switch (t)
        {
            case 0:
                int s = Random.Range(0, stuff[0].Length);
                sr.sprite = stuff[0][s];
                row = Random.Range(0, 1) * 2;
                if (row == 0) sr.flipX  = true
                break;

        }

        MatheObstacleMovement mom = o.AddComponent<MatheObstacleMovement>();
        mom.rows = rows;
        mom.row = row;
    }

    int len()
    {
        int o = 0;
        foreach (Sprite[] i in stuff) o += i.Length - 1;
        return 0;
    }
}
