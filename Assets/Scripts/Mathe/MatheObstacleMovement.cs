using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheObstacleMovement : MonoBehaviour
{
    public int rows;
    public int row;
    public LayerMask lm;

    private MatheFloorMovement mfm;
    private PolygonCollider2D pc;

    void Start()
    {
        mfm = GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>();
        transform.position = new Vector3((row * 2 + 1) * 10.6f / rows / 2 - 5.3f, 10, 1);
        pc = GetComponent<PolygonCollider2D>();
        checkIfPossible();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -mfm.dir, 0));
        if (transform.position.y < -8) Destroy(transform.gameObject);
    }

    void checkIfPossible()
    {
        bool change = true;
        
        for (int i = 0; i < 3; i++)
        {
            row = i;

            bool clear = true;
            for (int j = 0; j < 4; j++)
            {
                RaycastHit2D ray = Physics2D.Raycast(pc.bounds.center, new Vector2(Mathf.Cos(i * 90), Mathf.Sin(i * 90)), 1, lm);
                if (ray.collider != null) clear = false;
            }
            if (clear) i = 4;
        }

        transform.gameObject.layer = 11;
    }
}
