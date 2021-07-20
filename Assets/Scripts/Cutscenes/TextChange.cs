using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Video;

public class TextChange : MonoBehaviour
{
    public TextAsset text;
    public VideoClip[] videos;
    public string[] lines;

    private TMPro.TextMeshProUGUI tmp;
    private VideoPlayer video;

    void Start()
    {
        lines = text.text.Split('\n');
        tmp = GameObject.FindGameObjectWithTag("Overlay1").GetComponent<TMPro.TextMeshProUGUI>();
        video = GameObject.FindGameObjectWithTag("Overlay2").GetComponent<VideoPlayer>();
        StartCoroutine(routine());
    }

    WaitForSecondsRealtime ShowLine(int l)
    {
        string i = lines[l];
        string[] p = lines[l].Split('/');
        if (i.StartsWith("{"))
        {
            int z = int.Parse(p[0].Substring(1));
            video.enabled = true;
            video.clip = videos[z];

            return new WaitForSecondsRealtime((float)videos[z].length);
        }
        else
        {
            tmp.text = p[0];
            Debug.Log(p[0]);
            return new WaitForSecondsRealtime(int.Parse(p[1]));
        }
    }

    IEnumerator routine()
    {

        for (int i = 0; i < lines.Length; i++)
        {
            yield return ShowLine(i);
            video.enabled = false;
            if (Time.timeScale == 0) yield return new WaitWhile(() => Time.timeScale == 0);
        }
    }
}
