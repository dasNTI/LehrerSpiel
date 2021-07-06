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

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator shake()
    {
        float wait = 0.1f;
        for (int i = 0; i < 10; i++)
        {
            transform.position = Vector3.right * ((i % 2 * 2) - 1) * 0.07f + Vector3.forward * -8;
            yield return new WaitForSecondsRealtime(wait);
        }
        transform.position = Vector3.forward * -8;
    }
}
