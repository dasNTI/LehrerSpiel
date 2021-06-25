using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private BoxCollider2D bc;
    public LayerMask lm;
    public float width = 0.8f;
    public float jump = 1.2f;
    public AudioSource sound;
    private float min = 4;
    private int cool = 0;
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float extraHeight = 0.1f;
        Vector3 o = bc.bounds.center + Vector3.up * (bc.bounds.extents.y + extraHeight) + Vector3.left * width * bc.bounds.extents.x;
        RaycastHit2D ray = Physics2D.Raycast(o, Vector3.right, bc.bounds.size.x * width, lm);
        Debug.DrawRay(o, Vector3.right * bc.bounds.size.x * width);

        if (cool == 0) min = 4;

        if (ray.collider != null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            Vector2 v = p.GetComponent<Rigidbody2D>().velocity;
            p.GetComponent<PlayerMovement>().JumpAni();
            sound.Play();
            p.GetComponent<Rigidbody2D>().velocity = new Vector2(v.x, Mathf.Clamp(Mathf.Clamp(-v.y, min, 10) * jump, -10, 8.33f * jump));
            cool = 1000;
            min = p.GetComponent<Rigidbody2D>().velocity.y;
        }
    }
}
