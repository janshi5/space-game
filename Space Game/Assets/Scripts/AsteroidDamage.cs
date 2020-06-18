using UnityEngine;

public class AsteroidDamage : MonoBehaviour
{
    Rigidbody rb;
    public float health;
    public GameObject destroySoundPrefab;
    public GameObject[] powerUps;
    private AudioSource hitSound;

    // Start is called before the first frame update
    void Start()
    {
        health = transform.localScale.x * 100;
        rb = gameObject.GetComponent<Rigidbody>();
        hitSound = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Ally" ^ collisionInfo.gameObject.tag == "Enemy")
        {
            var velocityX = rb.velocity.x;
            if (velocityX < 0)
            {
                velocityX *= -1;
            }

            var velocityZ = rb.velocity.z;
            if (velocityZ < 0)
            {
                velocityZ *= -1;
            }

            int damage = (int)System.Math.Round(health * (velocityX + velocityZ) / 2);
            collisionInfo.gameObject.GetComponent<Health>().currentHealth -= damage;
            collisionInfo.gameObject.GetComponent<Health>().anim.SetTrigger("Hit");
            health -= damage / 25;

            hitSound.Play();
        }

        if (collisionInfo.gameObject.tag == "Asteroid")
        {
            health -= collisionInfo.gameObject.GetComponent<AsteroidDamage>().health / 2;

            if (collisionInfo.gameObject.transform.localScale.y > gameObject.transform.localScale.y)
            {
                hitSound.Play();
            }
        }
    }

    private void Update()
    {
        if (health <= 0 ^ (rb.velocity.x + rb.velocity.z > -1f && rb.velocity.x + rb.velocity.z < 1f))
        {
            if (Random.Range(0, 2) == 0)
            {
                var powerUp = Instantiate(powerUps[Random.Range(0, 2)]);
                powerUp.transform.position = transform.position;
                Vector3 movement = new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100));
                powerUp.GetComponent<Rigidbody>().AddForce(movement);
                Vector3 newRotation = new Vector3(0, Random.Range(0, 100), 0);
                powerUp.transform.Rotate(newRotation, Space.World);
            }
            Instantiate(destroySoundPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
