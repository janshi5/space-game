using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float shipSpeed;

    public float turnSpeed;

    public Rigidbody rb;

    public ParticleSystem boost;

    public Animator anim;

    float energy;

    Gradient defGradient;

    Gradient boostGradient;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        boost = GetComponentInChildren<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
        defGradient = new Gradient();
        defGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(1, 0.494f, 0.494f) , 0.047f), new GradientColorKey(new Color(0.592f, 0.08f, 0.08f), 0.347f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.447f), new GradientAlphaKey(0.3608f, 1.0f) });
        boostGradient = new Gradient();
        boostGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0.494f, 0.494f, 1), 0.047f), new GradientColorKey(new Color(0.08f, 0.08f, 0.592f), 0.347f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.447f), new GradientAlphaKey(0.3608f, 1.0f) });
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        energy = GameObject.Find("PlayerAimCircle").GetComponent<PlayerFire>().currentEnergy;

        var main = boost.main;

        if (Input.GetKey(KeyCode.LeftShift) && energy >= 5)
        {
            shipSpeed = -800f;
            GameObject.Find("PlayerAimCircle").GetComponent<PlayerFire>().currentEnergy -= 40 * Time.deltaTime;
            var col = boost.colorOverLifetime;
            col.color = boostGradient;
            if (energy <= 6)
            {
                GameObject.Find("PlayerAimCircle").GetComponent<PlayerFire>().currentEnergy -= 5;
            }
        }

        else
        {
            var col = boost.colorOverLifetime;
            col.color = defGradient;
            shipSpeed = -500f;
        }

        if (Input.GetKey("s"))
        {
            shipSpeed += 300f;
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
