using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Rigidbody rb;
    public Camera cam;
    public Transform shipPosition;

    Vector3 mousePos;

    private void Start()
    {
        cam = Camera.main;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        transform.position = shipPosition.position;
        Vector3 lookDir = new Vector3(mousePos.x, 0f, mousePos.z);
        transform.LookAt(lookDir);
    }
}
