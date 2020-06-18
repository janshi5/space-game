using UnityEngine;

public class FireAtButton : MonoBehaviour
{
    public Transform aim;
    public Transform firePoint;
    public GameObject laserPrefab;
    public GameObject laserSoundPrefab;
    public float laserForce = 20f;
    public Transform player;
    private void Update()
    {
        transform.position = player.position;
    }
    public void Fire(Transform buttonPos)
    {
        var pos = buttonPos.position;
        pos = Camera.main.ScreenToWorldPoint(buttonPos.position);
        pos = new Vector3(0f, 0f, pos.z);
        aim.LookAt(pos);
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
