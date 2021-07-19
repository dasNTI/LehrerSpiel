using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathtextChoose : MonoBehaviour
{
    public TextAsset texts;
    
    private TMPro.TextMeshProUGUI tmp;

    private void Start()
    {
        tmp = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void choose()
    {
        string[] lines = texts.text.Split('\n');
        tmp.text = lines[Random.Range(0, lines.Length)];
    }
}
