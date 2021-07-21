using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private RectTransform rt;
    private AudioSource[] Audio;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        Audio = GameObject.Find("Sounds").GetComponentsInChildren<AudioSource>();
    }

    void Update()
    {
        if (Pausing.Paused)
        {
            rt.anchoredPosition = Vector2.zero;
        }else
        {
            rt.anchoredPosition = Vector2.one * Screen.width * 10;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausing.setPaused(true);
            if (Pausing.Pausable) foreach (AudioSource i in Audio) i.Pause();
        }
    }

    public void resume()
    {
        Pausing.setPaused(false);
        foreach (AudioSource i in Audio) i.UnPause();
    }
}
