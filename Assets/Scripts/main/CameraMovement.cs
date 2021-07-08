using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D rb;
    public float Speed;
    public float AniSpeed;
    public float yoff;
    public float ymin = 0f;
    public bool follow = true;

    public Vector2 limit1 = new Vector2(-10, 0);
    public Vector2 limit2 = new Vector2(10, 0);

    public Vector2 dirSpeed = new Vector2(50f, 50f);
    public Vector2 dirSpeedAni = new Vector2(50f, 50f);

    private Vector3 fin;

    private Vector2 vel;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = Player.GetComponent<Rigidbody2D>();
        fin = transform.position;
        ymin = 2.24f;
        StartCoroutine(limit(limit1, limit2));
    }

    void Update()
    {
        Vector2 v = rb.velocity;

        float xdif = Player.transform.position.x - transform.position.x;
        float ydif = Mathf.Clamp(Player.transform.position.y, ymin, 10000) - transform.position.y + yoff;

        if (vel.x != v.x && follow)
        {
            for (float i = 0; i < Mathf.Abs(v.x - vel.x); i++)
            {
                float dif = v.x - vel.x;
                vel.x += dif / (200 - dirSpeed.x);
            }
        }
        
        if (vel.y != v.y && follow)
        {
            for (float i = 0; i < Mathf.Abs(v.y - vel.y); i++)
            {
                float dif = v.y - vel.y;
                vel.y += dif / (200 - dirSpeed.y);
            }
        }

        fin += new Vector3(xdif / (200 - Speed), ydif / (200 - Speed), 0);
        fin = new Vector3(
            Mathf.Clamp(fin.x, limit1.x, limit2.x),
            Mathf.Clamp(fin.y, limit1.y, limit2.y),
            fin.z
        );

        transform.position = fin;
    }

    public IEnumerator limit(Vector2 l1, Vector2 l2)
    {
        follow = false;

        limit1 = l1;
        limit2 = l2;

        float l = 0.1f;
        Vector2 dif = new Vector2(10, 10);
        while (true)
        {
            Vector2 v = rb.velocity;

            float xdif = Player.transform.position.x - transform.position.x;
            float ydif = Mathf.Clamp(Player.transform.position.y, ymin, 10000) - transform.position.y + yoff;

            if (vel.x != v.x)
            {
                for (float i = 0; i < Mathf.Abs(v.x - vel.x); i++)
                {
                    float d = v.x - vel.x;
                    vel.x += d / (200 - dirSpeedAni.x);
                }
            }

            if (vel.y != v.y)
            {
                for (float i = 0; i < Mathf.Abs(v.y - vel.y); i++)
                {
                    float d = v.y - vel.y;
                    vel.y += d / (200 - dirSpeedAni.y);
                }
            }

            dif = new Vector2(Mathf.Abs(v.x - vel.x), Mathf.Abs(v.y - vel.y));

            fin += new Vector3(xdif / (200 - AniSpeed), ydif / (200 - AniSpeed), 0);
            fin = new Vector3(
                Mathf.Clamp(fin.x, limit1.x, limit2.x),
                Mathf.Clamp(fin.y, limit1.y, limit2.y),
                fin.z
            );

            transform.position = fin;

            yield return new WaitForEndOfFrame();
        }

        follow = true;
    }
}
