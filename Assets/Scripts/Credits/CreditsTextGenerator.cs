using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsTextGenerator : MonoBehaviour
{
    public TextAsset credits;
    private TMPro.TextMeshProUGUI tmp;
    private RectTransform rt;
    public float speed;
    void Start()
    {
        tmp = GetComponent<TMPro.TextMeshProUGUI>();
        rt = GetComponent<RectTransform>();
        tmp.text = credits.text;
    }

    private void Update()
    {
        rt.anchoredPosition = new Vector2(
            rt.anchoredPosition.x,
            rt.anchoredPosition.y + speed
        );
    }
}
