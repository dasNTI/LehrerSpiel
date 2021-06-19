using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D rb;
    public float Speed;
    public float yoff;
    public float ymin = 0f;

    public Vector2 limit1 = new Vector2(-10, 0);
    public Vector2 limit2 = new Vector2(10, 0);

    public Vector2 dirSpeed = new Vector2(50f, 50f);

    private Vector3 fin;

    private Vector2 vel;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = Player.GetComponent<Rigidbody2D>();
        fin = transform.position;
        ymin = transform.position.y;
    }

    void Update()
    {
        Vector2 v = rb.velocity;

        float xdif = Player.transform.position.x - transform.position.x;
        float ydif = Mathf.Clamp(Player.transform.position.y, ymin, 10000) - transform.position.y + yoff;

        if (vel.x != v.x)
        {
            for (float i = 0; i < Mathf.Abs(v.x - vel.x); i++)
            {
                float dif = v.x - vel.x;
                vel.x += dif / (200 - dirSpeed.x);
            }
        }

        if (vel.y != v.y)
        {
            for (float i = 0; i < Mathf.Abs(v.y - vel.y); i++)
            {
                float dif = v.y - vel.y;
                vel.y += dif / (200 - dirSpeed.y);
            }
        }

        //xdif += Mathf.Clamp(vel.x / 10, -1, 1) * forecast.x;
        //ydif += Mathf.Clamp(vel.y / 10, -1, 1) * forecast.y;

        fin += new Vector3(xdif / (200 - Speed), ydif / (200 - Speed), 0);
        fin = new Vector3(
            Mathf.Clamp(fin.x, limit1.x, limit2.x),
            Mathf.Clamp(fin.y, limit1.y, limit2.y),
            fin.z
        );

        transform.position = fin;
    }
}
