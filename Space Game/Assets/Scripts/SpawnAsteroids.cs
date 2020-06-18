using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public Transform player;
    public float asteroidIntensity;
    public float asteroidForce;

    Transform asteroidSpawnPoint;
    float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;
        asteroidSpawnPoint = transform.Find("AsteroidSpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        if (cooldown <= 0)
        {
            SpawnAsteroid();
            cooldown = Random.Range(0, 5);
        }

        cooldown -= Time.deltaTime * asteroidIntensity;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            Destroy(other.gameObject);
        }
    }

    void SpawnAsteroid()
    {
        asteroidForce = Random.Range(20, 100);
        transform.Rotate(0f, Random.Range(-180, 180), 0f);
        GameObject asteroid = Instantiate(asteroidPrefabs[Random.Range(0, 4)], asteroidSpawnPoint.position, asteroidSpawnPoint.rotation);
        asteroid.transform.localScale *= Random.Range(0.2f, 1.5f);
        Rigidbody rb = asteroid.GetComponent<Rigidbody>();
        Vector3 asteroidVector = new Vector3(-asteroidForce, 0f, Random.Range(-80, 80));
        asteroid.transform.Rotate(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
        rb.AddRelativeForce(asteroidVector, ForceMode.Impulse);
    }
}
