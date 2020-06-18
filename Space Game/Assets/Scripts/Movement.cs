using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float shipSpeed;

    public float turnSpeed;

    public Rigidbody rb;

    public ParticleSystem boost;

    public Animator anim;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        boost = GetComponentInChildren<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        var main = boost.main;

        if (Input.GetKey("s"))
        {
            rb.AddRelativeForce(0.0f, shipSpeed * Time.deltaTime, 0.0f);

            main.startLifetime = 0.5f;
        }

        else if (Input.GetKey("w"))
        {
            rb.AddRelativeForce(0.0f, -shipSpeed * Time.deltaTime, 0.0f);

            main.startLifetime = 1f;
        }

        else
        {
            main.startLifetime = 0.7f;
        }

        var mainForce = boost.forceOverLifetime;
        AnimationCurve curve = new AnimationCurve();

        if (Input.GetKey("a"))
        {
            transform.Rotate(0.0f, 0.0f, turnSpeed * Time.deltaTime);
            curve.AddKey(0f, -3f);
            mainForce.x = new ParticleSystem.MinMaxCurve(1.5f, curve);
            anim.Play("TurnLeft");
        }

        else if (Input.GetKey("d"))
        {
            transform.Rotate(0.0f, 0.0f, -turnSpeed * Time.deltaTime);
            curve.AddKey(0f, 3f);
            mainForce.x = new ParticleSystem.MinMaxCurve(1.5f, curve);
            anim.Play("TurnRight");
        }

        else
        {
            curve.AddKey(0f, 0f);
            mainForce.x = new ParticleSystem.MinMaxCurve(1.5f, curve);
            anim.Play("Idle");
        }

    }
}
