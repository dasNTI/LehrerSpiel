using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Drawing;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Maske : MonoBehaviour
{
    bool check = true;
    public bool on = false;

    private SpriteRenderer sr;

    public LayerMask lm;
    private BoxCollider2D bc;

    private Moves moves;

    public VolumeProfile vp;

    public string MaskButton = "f";

    bool alerting = false;
    int alert = 0;
    int alertingresetcooldown = 0;
    private Image im;
    private TMPro.TextMeshProUGUI tx;
    private TMPro.TextMeshProUGUI tx1;
    private Coroutine OverlayFadeAni;

    public AudioSource Alert;
    public AudioSource Whistle;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();

        tx = GameObject.FindGameObjectWithTag("Overlay2").GetComponent<TMPro.TextMeshProUGUI>();
        tx1 = GameObject.FindGameObjectWithTag("Overlay3").GetComponent<TMPro.TextMeshProUGUI>();
        im = GameObject.FindGameObjectWithTag("Overlay1").GetComponent<Image>();

        moves = new Moves();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(MaskButton) != check)
        {
            check = Input.GetKey(MaskButton);
            if (check) changeOn();
        }

        sr.enabled = on;

        if (alertingresetcooldown > 0) alertingresetcooldown--;
        if (alertingresetcooldown == 0) alerting = false;
    }

    public void changeOn()
    {
        if (!on)
        {
            on = true;

            if (alerting)
            {
                alerting = false;
                alert = 0;
                if (OverlayFadeAni != null) StopCoroutine(OverlayFadeAni);
                StartCoroutine(OverlayAni1(1, -1, .2f));
            }
        }
        else if (on)
        {
            on = false;
        }
    }

    bool CheckAbilityAvailable()
    {
        int len = 10;
        int rays = 20;
        float mul = 360 / rays;
        bool o = false;
        for (int i = 0; i <= rays; i++)
        {
            RaycastHit2D ray = Physics2D.Raycast(bc.bounds.center, new Vector2(Mathf.Cos(i * mul), Mathf.Sin(i * mul)), len, lm);
            Debug.DrawRay(bc.bounds.center, new Vector2(Mathf.Cos(i * mul), Mathf.Sin(i * mul)) * len);
            if (ray.collider != null) o = true;
        }
        return o;
    }

    public void emit()
    {
        if (!alerting && !on)
        {
            alerting = true;
            alertingresetcooldown = 100;
            Alert.Play();
            alert = 0;

            string[] r = "e r t z u i o p l k j m n h b g v f c x y q".Split(' ');
            MaskButton = r[Random.Range(0, r.Length)];

            OverlayFadeAni = StartCoroutine(OverlayAni1(0, 1, 0.2f));
            StartCoroutine(OverlayAni2());
        }
    }

    IEnumerator OverlayAni1(float v1, float v2, float dur)
    {

        tx.color = new UnityEngine.Color(255, 0, 0, v1);
        tx1.color = new UnityEngine.Color(255, 0, 0, v1);
        tx1.text = "[ " + MaskButton.ToUpper() + " ] drücken!";
        for (int i = 0; i < 20; i++)
        {
            tx.color = new UnityEngine.Color(255, 0, 0, tx.color.a + v2 / 20);
            tx1.color = new UnityEngine.Color(255, 0, 0, tx1.color.a + v2 / 20);
            yield return new WaitForSecondsRealtime(dur / 20);
        }
        OverlayFadeAni = null;
    }

    IEnumerator OverlayAni2()
    {
        float dur = 2;
        im.color = new UnityEngine.Color(255, 0, 0, 0);
        while (!on && alert < 100 && alerting)
        {
            if (alerting) im.color = new UnityEngine.Color(255, 0, 0, im.color.a + 0.01f);
            yield return new WaitForSecondsRealtime(dur / 100);
        }
        im.color = new UnityEngine.Color(255, 0, 0, 0);
        if (alert >= 100)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().ResetToCheckpoint();
            Whistle.Play();
            StartCoroutine(OverlayAni1(1, -1, 0.1f));
        }
    }
}
