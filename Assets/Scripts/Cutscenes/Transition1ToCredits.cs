using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition1ToCredits : MonoBehaviour
{
    private Transition1 trans;

    private void Start()
    {
        trans = GameObject.Find("Transition1").GetComponent<Transition1>();
    }

    public void go(string name)
    {
        StartCoroutine(g(name));
    }

    IEnumerator g(string name)
    {
        StartCoroutine(trans.transOut());
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }
}
