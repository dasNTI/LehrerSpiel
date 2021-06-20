using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float JumpHeight = 1;
    private Rigidbody2D rb;
    private Moves moves;
    public bool gamepad = false;
    public float MaxSpeed = 1;
    private PolygonCollider2D bc;
    public LayerMask lm;

    private bool walking = false;
    public float walkCool = 0.3f;

    public AudioSource[] Sounds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moves = new Moves();
        //moves.mode = "manual";
        bc = GetComponent<PolygonCollider2D>();
        StartCoroutine(walkSound());
        StartCoroutine(routin());
    }


    void Update()
    {
        if (moves.side() != 0)
        {
            if (!TouchingSide(moves.side()))
            {
                rb.velocity = new Vector2(moves.side() * MaxSpeed, rb.velocity.y);
            }else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            walking = true;
            GameObject.FindGameObjectWithTag("Schloesser_Head").GetComponent<SchlösserEyeMovement>().dir = 1;
            if (moves.side() < 0)
            {
                transform.localScale = new Vector3(
                    -Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }else
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("Schloesser_Head").GetComponent<SchlösserEyeMovement>().dir = 0;
            rb.velocity = new Vector2(0, rb.velocity.y);
            walking = false;
        }

        if (moves.side() != 0 && TouchingGround())
        {
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Schloesser_Arm"))
            {
                i.GetComponent<Animator>().SetFloat("Blend", 0.5f);
                i.GetComponent<Animation>().Stop();
            }

            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Schloesser_UpperLeg"))
            {
                i.GetComponent<Animator>().SetFloat("Blend", 1);
                i.GetComponent<Animation>().Stop();
            }

            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Schloesser_LowerLeg"))
            {
                i.GetComponent<Animator>().SetFloat("Blend", 1);
                i.GetComponent<Animation>().Stop();
            }

            GameObject.FindGameObjectWithTag("Schloesser_Torso").GetComponent<Animator>().SetFloat("Blend", 1);
            GameObject.FindGameObjectWithTag("Schloesser_Torso").GetComponent<Animation>().Stop();
        }
        else if (moves.side() == 0 && TouchingGround())
        {
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Schloesser_Arm"))
            {
                i.GetComponent<Animator>().SetFloat("Blend", 0);
            }

            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Schloesser_UpperLeg"))
            {
                i.GetComponent<Animator>().SetFloat("Blend", 0);
            }

            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Schloesser_LowerLeg"))
            {
                i.GetComponent<Animator>().SetFloat("Blend", 0);
            }

            GameObject.FindGameObjectWithTag("Schloesser_Torso").GetComponent<Animator>().SetFloat("Blend", 0);
        }

        if (moves.jump()) Jump();
    }

    void Jump()
    {
        if (TouchingGround())
        {

            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Schloesser_Arm"))
            {
                i.GetComponent<Animation>().Stop();
                i.GetComponent<Animation>().Play();
                i.GetComponent<Animator>().SetFloat("Blend", 0);
            }

            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Schloesser_UpperLeg"))
            {
                i.GetComponent<Animation>().Stop();
                i.GetComponent<Animation>().Play();
                i.GetComponent<Animator>().SetFloat("Blend", 0);
            }

            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Schloesser_LowerLeg"))
            {
                i.GetComponent<Animation>().Stop();
                i.GetComponent<Animation>().Play();
                i.GetComponent<Animator>().SetFloat("Blend", 0);
            }

            GameObject.FindGameObjectWithTag("Schloesser_Torso").GetComponent<Animator>().SetFloat("Blend", 0);
            GameObject.FindGameObjectWithTag("Schloesser_Torso").GetComponent<Animation>().Play();

            rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
            Sounds[0].Play();
        }
    }

    bool TouchingSide(float dir)
    {
        bool outp = false;

        /*int ray = 100;
        RaycastHit2D[] rays = new RaycastHit2D[ray + 1];
        float extra = 0;
        float yoffset = 1;
        for (int i = 0; i < ray + 1; i++)
        {
            rays[i] = Physics2D.Raycast(bc.bounds.center + Vector3.up*(bc.bounds.extents.y + yoffset) - new Vector3(0, i * (bc.bounds.size.y / ray)), Vector2.right, 1, lm);
            Debug.DrawRay(bc.bounds.center + Vector3.up * bc.bounds.extents.y - new Vector3(0, i * (bc.bounds.size.y / ray)), Vector2.right);
        }

        
        foreach (RaycastHit2D r in rays)
        {
            if (r.collider != null)
            {
                outp = true;
                Debug.Log(r.collider.gameObject);
            }
        }*/

        //lm = ~1 << 9;

        float extrawidth = 0.1f;

        RaycastHit2D ray = Physics2D.Raycast(bc.bounds.center + Vector3.up * bc.bounds.extents.y + Vector3.right * dir.CompareTo(0) * (bc.bounds.extents.x + extrawidth), Vector2.down * bc.bounds.size.y, bc.bounds.size.y, lm);
        Debug.DrawRay(bc.bounds.center + Vector3.up * bc.bounds.extents.y + Vector3.right * dir.CompareTo(0) * bc.bounds.extents.x, Vector2.down * bc.bounds.size.y);

        outp = ray.collider != null;

        return outp;
    }
    bool TouchingGround()
    {
        float extraHeight = 0.1f;
        float offset = -0.5f;

        RaycastHit2D ray1 = Physics2D.Raycast(bc.bounds.center + new Vector3(bc.bounds.extents.x + offset, 0, 0), Vector2.down, bc.bounds.extents.y + extraHeight, lm);
        RaycastHit2D ray2 = Physics2D.Raycast(bc.bounds.center - new Vector3(bc.bounds.extents.x + offset, 0, 0), Vector2.down, bc.bounds.extents.y + extraHeight, lm);
        RaycastHit2D ray3 = Physics2D.Raycast(bc.bounds.center, Vector2.down, bc.bounds.extents.y + extraHeight, lm);

        return ray1.collider != null || ray2.collider != null || ray3.collider != null;
    }

    IEnumerator walkSound()
    {
        do
        {
            yield return new WaitUntil(() => walking);
            while (walking)
            {
                if (TouchingGround())
                {
                    int r = Random.Range(1, 3);
                    Sounds[r].panStereo = Mathf.Clamp(Random.RandomRange(-2f, 2f), -1, 1);
                    Sounds[r].Play();
                }
                yield return new WaitForSecondsRealtime(walkCool);
            }
        } while (true);
    }

    IEnumerator routin()
    {
        yield return new WaitForSecondsRealtime(3);
        moves.sid = 1;
        yield return new WaitForSecondsRealtime(3);
        moves.sid = -1;
        yield return new WaitForSecondsRealtime(3);
        moves.sid = 0;
        yield return new WaitForSecondsRealtime(1);
        moves.jum = true;
        yield return new WaitForSecondsRealtime(3);
        moves.jum = false;
    }
}