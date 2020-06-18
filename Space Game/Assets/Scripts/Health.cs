using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;

    public HealthBar healthBar;
    public HealthBar healthBarUI;
    public Animator anim;
    public GameObject DestroySoundPrefab;
    public GameObject[] powerUps;
    public AudioSource hitSound;
    public GameObject aimCircle;
    public GameObject healthBarCanvas;
    public int anger;
    public Material yellowShip;
    public Material yellowEnergy;

    // Start is called before the first frame update
    void Start()
    {
        hitSound = gameObject.GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anger = 0;

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Laser")
        {
            currentHealth -= 200;

            anim.SetTrigger("Hit");

            hitSound.Play();

            if (collisionInfo.collider.GetComponent<ProjectileTimer>().shotBy == "PlayerAimCircle" && gameObject.tag == "Ally")
            {
                anger += 1;
            }

            else if (anger < 3)
            {
                anger = 0;
            }
        }
        
    }

    void Update()
    {
        if (anger >= 3)
        {
            gameObject.transform.Find("SpaceShip").GetComponent<MeshRenderer>().material = yellowShip;
            gameObject.transform.Find("SpaceShip").transform.Find("ShipEnergy").GetComponent<MeshRenderer>().material = yellowEnergy;
            aimCircle.GetComponent<Fire>().colorR = 1f;
            aimCircle.GetComponent<Fire>().colorG = 1f;
            aimCircle.GetComponent<Fire>().colorB = 0f;
        }

        if (currentHealth <= 0)
        {
            if (gameObject.name == "Player")
            {
                if (GameManager.playerLife == 1)
                {
                    FindObjectOfType<GameManager>().GameOver();
                }

                else
                {
                    GameManager.playerLife -= 1;
                    PlayerReset();
                }
            }
            else
            {
                if (gameObject.name == "GeneralRed")
                {
                    GameManager.generalRed = false;
                    if (Random.Range(0,2) == 0)
                    {
                        var powerUp = Instantiate(powerUps[1]);
                        powerUp.transform.position = transform.position;
                        Vector3 movement = new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100));
                        powerUp.GetComponent<Rigidbody>().AddForce(movement);
                        Vector3 newRotation = new Vector3(0, Random.Range(0, 100), 0);
                        powerUp.transform.Rotate(newRotation, Space.World);
                    }
                }

                else if (gameObject.name == "GeneralGreen")
                {
                    GameManager.generalGreen = false;
                    if (Random.Range(0, 2) == 0)
                    {
                        var powerUp = Instantiate(powerUps[0]);
                        powerUp.transform.position = transform.position;
                        Vector3 movement = new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100));
                        powerUp.GetComponent<Rigidbody>().AddForce(movement);
                        Vector3 newRotation = new Vector3(0, Random.Range(0, 100), 0);
                        powerUp.transform.Rotate(newRotation, Space.World);
                    }
                }
                Instantiate(DestroySoundPrefab, transform.position, transform.rotation);
                Destroy(aimCircle);
                Destroy(healthBarCanvas);
                Destroy(gameObject);
            }
        }

        healthBar.SetHealth(currentHealth);
        if (gameObject.name == "Player")
        {
            healthBarUI.SetHealth(currentHealth);
        }
    }

    public void GetHit(string attacker)
    {
        currentHealth -= 200;

        anim.SetTrigger("Hit");

        hitSound.Play();

        if (attacker == "PlayerAimCircle" && gameObject.tag == "Ally")
        {
            anger += 1;
        }

        else if (anger < 3)
        {
            anger = 0;
        }
    }

    private void PlayerReset()
    {
        Transform target = gameObject.transform;
        float distance = 99999999;
        var planets = GameObject.FindGameObjectsWithTag("GreenPlanet");
        foreach (var planet in planets)
        {
            float temp1 = planet.transform.position.x - gameObject.transform.position.x;
            float temp2 = planet.transform.position.z - gameObject.transform.position.z;
            if (temp1 < 0)
            {
                temp1 *= -1;
            }

            if (temp2 < 0)
            {
                temp2 *= -1;
            }

            if (distance > temp1 + temp2)
            {
                distance = temp1 + temp2;
                target = planet.transform;
            }
        }

        gameObject.transform.position = new Vector3(target.position.x, 0f, target.position.z);
        currentHealth = maxHealth;
        GameObject.Find("PlayerAimCircle").GetComponent<PlayerFire>().currentEnergy = GameObject.Find("PlayerAimCircle").GetComponent<PlayerFire>().maxEnergy;
    }
}
