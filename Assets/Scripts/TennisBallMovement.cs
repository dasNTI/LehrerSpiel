using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBallMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PolygonCollider2D pc;
    public AudioSource a;
    public LayerMask lm;

    private bool t = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 20 && t) Destroy(rb.gameObject);

        RaycastHit2D ray = Physics2D.Raycast(pc.bounds.center + Vector3.down * pc.bounds.extents.y, Vector3.down, pc.bounds.extents.y + 0.01f, lm);
        if (ray.collider != null && !t)
        {
            t = true;
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y * 1.5f);
        }
    }
}
