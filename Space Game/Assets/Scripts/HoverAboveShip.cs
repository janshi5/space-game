using UnityEngine;
using UnityEngine.UIElements;

public class HoverAboveShip : MonoBehaviour
{
    public Transform shipPosition;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(shipPosition.position.x, shipPosition.position.y, shipPosition.position.z + 1);

        transform.position = newPosition;

       
    }
}
