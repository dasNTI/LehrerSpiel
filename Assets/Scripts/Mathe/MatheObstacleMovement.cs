using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheObstacleMovement : MonoBehaviour
{
    public int rows;
    public int row;
    private MatheFloorMovement mfm;
    void Start()
    {
        mfm = GameObject.FindGameObjectWithTag("Player").GetComponent<MatheFloorMovement>();
        transform.position = new Vector3((row * 2 + 1) * 11 / rows / 2 - 5.5f, 7.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -mfm.dir, 0));
        if (transform.position.y < -8) Destroy(transform.gameObject);
    }
}
