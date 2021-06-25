using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tennis : MonoBehaviour
{
    public int balls = 10;
    public Sprite sprite;
    public AudioSource a;
    private GameObject p;
    public LayerMask lm;
    public float width = 15;

    private void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player");
    }

    public IEnumerator spawn()
    {
        int no = Random.Range(0, balls);
        //GameObject[] bal;
        for (int i = 0; i < balls; i++)
        {
            if (i != no)
            {
                GameObject ball = new GameObject("TennisBall");
                SpriteRenderer sr = ball.AddComponent<SpriteRenderer>();
                sr.sprite = sprite;
                PolygonCollider2D pol = ball.AddComponent<PolygonCollider2D>();
                Rigidbody2D rb = ball.AddComponent<Rigidbody2D>();
                rb.velocity = new Vector2(-1, 0);
                TennisBallMovement tbm = ball.AddComponent<TennisBallMovement>();
                tbm.a = a;
                tbm.lm = lm;



                ball.transform.position = p.transform.position + Vector3.up * 30 + Vector3.left * width / 2 + Vector3.right * width / balls * i;

                yield return new WaitForSecondsRealtime(0.2f);
            }
        }
    }
}
