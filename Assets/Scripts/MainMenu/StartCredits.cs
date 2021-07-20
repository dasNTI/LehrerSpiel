using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCredits : MonoBehaviour
{
    public Transition1 trans;

    void Start()
    {
        trans = GameObject.Find("Transition1").GetComponent<Transition1>();
    }

    public void go()
    {
        StartCoroutine(g());
    }

    IEnumerator g()
    {
        StartCoroutine(trans.transIn());
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }
}
