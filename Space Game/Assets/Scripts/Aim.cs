using UnityEngine;

public class Aim : MonoBehaviour
{
    public Transform target;
    public Transform shipPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = shipPosition.position;
        try
        {
            transform.LookAt(target.position);
        }
        catch
        {
         
        }
    }
}
