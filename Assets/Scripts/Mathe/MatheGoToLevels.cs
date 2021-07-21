using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatheGoToLevels : MonoBehaviour
{
    private Transition0 trans;
    void Start()
    {
        trans = GetComponent<Transition0>();
    }

    public void go(string name)
    {
        StartCoroutine(g(name));
    }

    IEnumerator g(string name)
    {
        StartCoroutine(trans.transIn());
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

}
