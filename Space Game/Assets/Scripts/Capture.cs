using UnityEngine;
using UnityEngine.UI;

public class Capture : MonoBehaviour
{
    public float maxCapturePoints;
    public float currentCapturePoints;
    public float captureSpeed;
    public float captureRegainSpeed;
    public GameObject enemyPlanetPrefab;
    public Image captureProgress;
    public bool isBeingCaptured;
    string enemy;
    // Start is called before the first frame update
    void Start()
    {
        isBeingCaptured = false;
        if (gameObject.tag == "GreenPlanet")
        {
            enemy = "Enemy";
        }
        else if (gameObject.tag == "RedPlanet")
        {
            enemy = "Ally";
        }
        currentCapturePoints = 0f;
        captureProgress = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        captureSpeed = 0f;
        var enemies = GameObject.FindGameObjectsWithTag(enemy);
        foreach (var ship in enemies)
        {
            if (gameObject.transform.position.x + 3f > ship.transform.position.x && ship.transform.position.x > gameObject.transform.position.x - 3f)
            {
                if (gameObject.transform.position.z + 3f > ship.transform.position.z && ship.transform.position.z > gameObject.transform.position.z - 3f)
                {
                    if (ship.name == "Player")
                    {
                        captureSpeed += 10f;
                    }
                    else
                    {
                        captureSpeed += 5f;
                    }
                }
            }
        }

        currentCapturePoints += captureSpeed * Time.deltaTime;

        if (currentCapturePoints > 0 && captureSpeed == 0)
        {
            currentCapturePoints -= Time.deltaTime * captureRegainSpeed;
        }

        if (currentCapturePoints >= maxCapturePoints && enemy == "Ally")
        {
            GameObject.Instantiate(enemyPlanetPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (currentCapturePoints >= maxCapturePoints && enemy == "Enemy")
        {
            GameObject.Instantiate(enemyPlanetPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        captureProgress.fillAmount = currentCapturePoints / maxCapturePoints;

        if (currentCapturePoints > 0)
        {
            isBeingCaptured = true;
        }

        else
        {
            isBeingCaptured = false;
        }
    }
}
