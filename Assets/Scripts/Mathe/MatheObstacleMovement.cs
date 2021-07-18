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

        float len = 2.5f;

        bool clear = true;

        bool c()
        {
            RaycastHit2D ray1 = Physics2D.Raycast(pc.bounds.center + Vector3.up * pc.bounds.extents.y * 1.5f, Vector2.left, len, lm);
            RaycastHit2D ray2 = Physics2D.Raycast(pc.bounds.center - Vector3.up * pc.bounds.extents.y * 1.5f, Vector2.left, len, lm);
            RaycastHit2D ray3 = Physics2D.Raycast(pc.bounds.center + Vector3.up * pc.bounds.extents.y * 1.5f, Vector2.right, len, lm);
            RaycastHit2D ray4 = Physics2D.Raycast(pc.bounds.center - Vector3.up * pc.bounds.extents.y * 1.5f, Vector2.right, len, lm);

            return !(ray1.collider != null || ray2.collider != null || ray3.collider != null || ray4.collider != null);
        }

        if (row == 1)
        {
            RaycastHit2D ray1 = Physics2D.Raycast(pc.bounds.center + Vector3.up * pc.bounds.extents.y * 1.5f, Vector2.left, len, lm);
            RaycastHit2D ray2 = Physics2D.Raycast(pc.bounds.center - Vector3.up * pc.bounds.extents.y * 1.5f, Vector2.left, len, lm);
            RaycastHit2D ray3 = Physics2D.Raycast(pc.bounds.center + Vector3.up * pc.bounds.extents.y * 1.5f, Vector2.right, len, lm);
            RaycastHit2D ray4 = Physics2D.Raycast(pc.bounds.center - Vector3.up * pc.bounds.extents.y * 1.5f, Vector2.right, len, lm);

            if (ray1.collider != null || ray2.collider != null || ray3.collider != null || ray4.collider != null) Destroy(transform.gameObject);

        }

        if (!c())
        {
            Destroy(transform.gameObject);
        }

        transform.gameObject.layer = 11;
    }
}
