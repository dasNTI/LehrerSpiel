using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidDistanceCheck : MonoBehaviour
{
    private PolygonCollider2D pc;
    public LayerMask lm;
    void Start()
    {
        pc = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(check());
    }

    bool check()
    {
        int len = 10;
        int rays = 20;
        float mul = 360 / rays;
        bool o = false;
        for (int i = 0; i <= rays; i++)
        {
            RaycastHit2D ray = Physics2D.Raycast(pc.bounds.center, new Vector2(Mathf.Cos(i * mul), Mathf.Sin(i * mul)), len, lm);
            Debug.DrawRay(pc.bounds.center, new Vector2(Mathf.Cos(i * mul), Mathf.Sin(i * mul)) * len);
            if (ray.collider != null) o = true;
        }
        return o;
    }
}
