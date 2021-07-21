using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatheProgressMovement : MonoBehaviour
{
    private RectTransform rt;
    private MatheWayCounter mwc;
    private Image im;

    public float s = 545;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        mwc = GameObject.FindGameObjectWithTag("Player").GetComponent<MatheWayCounter>();
        im = GetComponent<Image>();

        s = GameObject.Find("ProgressBar").GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        float f = mwc.Current / mwc.FinishLine;

        rt.anchoredPosition = Vector2.up * f * s;

        if (f > (5 / 6f))
        {
            GameObject.Find("ProgressBarFinish").GetComponent<Animator>().SetBool("active", true);
        }else
        {
            GameObject.Find("ProgressBarFinish").GetComponent<Animator>().SetBool("active", false);
        }

        if (f > (11 / 12f))
        {
            float c = (f - 11 / 12f) * 12;
            //Debug.Log(c);
        }else
        {
            im.color = new Color(255, 255, 255, 1);
        }
    }                                                                                                         
}                                                                                                             
                                                                                                              