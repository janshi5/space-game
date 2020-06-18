using UnityEditor;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject laserPrefab;
    public GameObject laserSoundPrefab;
    public float laserForce = 20f;
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float energyRegenSpeed;
    public MeshRenderer shipEnergy;
    public Color energyColor;
    public float coolDown;
    float colorR;
    float colorG;
    float colorB;

    // Update is called once per frame

    private void Start()
    {
        currentEnergy = maxEnergy;
        firePoint = gameObject.transform.Find("FirePoint");
        energyColor = shipEnergy.material.color;
        colorR = energyColor.r;
        colorG = energyColor.g;
        colorB = energyColor.b;
    }
    void Update()
    {

        if (currentEnergy < maxEnergy)
        {
            currentEnergy += Time.deltaTime * energyRegenSpeed;
            energyColor.r += Time.deltaTime * energyRegenSpeed / 100;
            energyColor.g += Time.deltaTime * energyRegenSpeed / 100;
            energyColor.b += Time.deltaTime * energyRegenSpeed / 100;
        }

        if (Input.GetButton("Fire1") && currentEnergy >= 10 && coolDown <= 0 && HideShowPowerUp.isOverUI == false && GameManager.powerupHeld == false)
        {
            Shoot();
            coolDown = 0.1f;
            currentEnergy -= 10;
        }

        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;

        }

        energyColor.r = colorR * currentEnergy / 100;
        energyColor.g = colorG * currentEnergy / 100;
        energyColor.b = colorB * currentEnergy / 100;
        shipEnergy.material.color = energyColor;
    }

    void Shoot()
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        GameObject laserSound = Instantiate(laserSoundPrefab, firePoint.position, firePoint.rotation);
        laserSound.GetComponent<FollowLaser>().bullet = laser.transform;
        laser.GetComponent<ProjectileTimer>().shotBy = gameObject.name;
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        Transform laserPos = laser.GetComponent<Transform>();
        Vector3 laserRotation = new Vector3(laserPos.rotation.x, laserPos.rotation.y + 90f, laserPos.rotation.z);
        laserPos.Rotate(laserRotation);
        Vector3 laserVector = new Vector3(0f, 0f, laserForce);
        rb.AddRelativeForce(laserVector, ForceMode.Impulse);
    }
}