using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheWayCounter : MonoBehaviour
{
    public int FinishLine;
    public float Current = 0;
    private MathePlayerMovement mpm;
    private MatheFloorMovement mfm;
    void Start()
    {
        mpm = GetComponent<MathePlayerMovement>();
        mfm = GameObject.FindGameObjectWithTag("Ground").GetComponent<MatheFloorMovement>();
    }

    
    void Update()
    {
        Current += mfm.dir / 10;
    }
}
