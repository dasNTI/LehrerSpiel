using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheProgressMovement : MonoBehaviour
{
    private RectTransform rt;
    private MatheWayCounter mwc;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        mwc = GameObject.FindGameObjectWithTag("Player").GetComponent<MatheWayCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        float f = mwc.Current / mwc.FinishLine;

        rt.anchoredPosition3D = Vector3.up * f * 659;
    }
}
