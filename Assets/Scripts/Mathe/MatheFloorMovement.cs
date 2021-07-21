using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheFloorMovement : MonoBehaviour
{
    public float speed = 2;
    public int change = 170;
    public float dir = 0;
    public Vector3 start, stop;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dir != speed)
        {
            for (float i = 0; i < Mathf.Abs(speed - dir); i++)
            {
                float dif = speed - dir;
                dir += dif / (200 - change);
            }
        }

        if (!Pausing.Paused) transform.Translate(new Vector3(0, -dir, 0));
        if (transform.position.y <= stop.y) transform.position = start;
    }
}
