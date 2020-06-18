using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject shipPrefab;
    public GameObject aimPrefab;
    public GameObject healthPrefab;
    public Vector3 spawnPosition;
    private float coolDown;

    void Start()
    {
        coolDown = Random.Range(2, 10);
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;

        if (coolDown <= 0)
        {
            SpawnShip();
            coolDown = Random.Range(10, 30);
        }
    }

    private void SpawnShip()
    {
        gameObject.GetComponent<AudioSource>().Play();
        spawnPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        GameObject health = Instantiate(healthPrefab, spawnPosition, transform.rotation);
        GameObject ship = Instantiate(shipPrefab, spawnPosition, transform.rotation);
        GameObject aim = Instantiate(aimPrefab, spawnPosition, transform.rotation);

        if (gameObject.tag == "RedPlanet" && GameManager.generalRed == false)
        {
            if (Random.Range(0, 5) == 4)
            {
                GameManager.generalRed = true;
                ship.name = "GeneralRed";
                ship.transform.localScale *= 1.5f;
                aim.transform.localScale *= 1.5f;
                ship.GetComponent<Health>().maxHealth = 2000;
                aim.GetComponent<Fire>().maxEnergy = 150f;
            }
        }

        else if (gameObject.tag == "GreenPlanet" && GameManager.generalGreen == false)
        {
            if (Random.Range(0, 10) == 4)
            {
                GameManager.generalGreen = true;
                ship.name = "GeneralGreen";
                ship.transform.localScale *= 1.5f;
                aim.transform.localScale *= 1.5f;
                ship.GetComponent<Health>().maxHealth = 2000;
                aim.GetComponent<Fire>().maxEnergy = 150f;
            }
        }

        ship.GetComponent<AI_Movement>().aim = aim.transform;
        health.GetComponentInChildren<HoverAboveShip>().shipPosition = ship.transform;
        ship.GetComponent<Health>().healthBar = health.GetComponentInChildren<HealthBar>();
        ship.GetComponent<Health>().anim = health.GetComponentInChildren<Animator>();
        health.GetComponent<HoverAboveShip>().shipPosition = ship.transform;
        aim.GetComponent<Aim>().shipPosition = ship.transform;
        ship.GetComponent<Health>().aimCircle = aim;
        ship.GetComponent<Health>().healthBarCanvas = health;
        GameObject shipModel = ship.transform.Find("SpaceShip").gameObject;
        GameObject shipEnergy = shipModel.transform.Find("ShipEnergy").gameObject;
        aim.GetComponent<Fire>().shipEnergy = shipEnergy.GetComponent<MeshRenderer>();
    }
}
