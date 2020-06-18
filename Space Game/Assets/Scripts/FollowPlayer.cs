using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform shipPosition;

    public Vector3 cameraPosition;

    public Vector3 offset;

    public float smoothSpeed = 0.125f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = shipPosition.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
