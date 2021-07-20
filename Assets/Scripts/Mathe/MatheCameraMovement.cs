using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatheCameraMovement : MonoBehaviour
{
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public IEnumerator shake()
    {
        float wait = 0.1f;
        for (int i = 0; i < 10; i++)
        {
            transform.position = Vector3.right * ((i % 2 * 2) - 1) * 0.07f + Vector3.forward * -8;
            yield return new WaitForSecondsRealtime(wait);
            if (Time.timeScale == 0) yield return new WaitWhile(() => Time.timeScale == 0);
        }
        transform.position = Vector3.forward * -8;
    }
}
