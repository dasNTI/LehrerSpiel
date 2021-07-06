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
        mfm = GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>();
        transform.position = new Vector3((row * 2 + 1) * 10.6f / rows / 2 - 5.3f, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -mfm.dir, 0));
        if (transform.position.y < -8) Destroy(transform.gameObject);
    }
}
