using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public Transform shipPosition;

    public Vector3 oldShipPosition;

    public Vector3 backgroundPosition;

    public float backgroundSpeed;

    public void Start()
    {
        oldShipPosition = shipPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Remove background if player gets too far and create a new background in front of player

        if  (shipPosition.position.x - transform.position.x > 30)
        {
            backgroundPosition = new Vector3(transform.position.x + 57.6f, transform.position.y, transform.position.z);
            transform.position = backgroundPosition;
        }

        else if (shipPosition.position.z - transform.position.z > 21.6)
        {
            backgroundPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 32.4f);
            transform.position = backgroundPosition;
        }

        else if (transform.position.x - shipPosition.position.x > 30)
        {
            backgroundPosition = new Vector3(transform.position.x - 57.6f, transform.position.y, transform.position.z);
            transform.position = backgroundPosition;
        }

        else if (transform.position.z - shipPosition.position.z > 21.6)
        {
            backgroundPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 32.4f);
            transform.position = backgroundPosition;
        }
    }

    private void FixedUpdate()
    {

        if (shipPosition.position.x > oldShipPosition.x)
        {
            backgroundPosition = new Vector3(transform.position.x + (shipPosition.position.x - oldShipPosition.x) * backgroundSpeed, transform.position.y, transform.position.z);
            transform.position = backgroundPosition;
        }

        if (shipPosition.position.x < oldShipPosition.x)
        {
            backgroundPosition = new Vector3(transform.position.x - (oldShipPosition.x - shipPosition.position.x) * backgroundSpeed, transform.position.y, transform.position.z);
            transform.position = backgroundPosition;
        }

        if (shipPosition.position.z > oldShipPosition.z)
        {
            backgroundPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + (shipPosition.position.z - oldShipPosition.z) * backgroundSpeed);
            transform.position = backgroundPosition;
        }

        if (shipPosition.position.z < oldShipPosition.z)
        {
            backgroundPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - (oldShipPosition.z - shipPosition.position.z) * backgroundSpeed);
            transform.position = backgroundPosition;
        }

        oldShipPosition = shipPosition.position;
    }
}
