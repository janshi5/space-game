using UnityEngine;

public class Pointer : MonoBehaviour
{
    Transform player;
    Transform target;
    RectTransform pointerPosition;
    float distanceX;
    float distanceZ;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        pointerPosition = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;

        float distance = 9999999f;

        if (gameObject.name == "RedPointerCircle")
        {
            var redPlanets = GameObject.FindGameObjectsWithTag("RedPlanet");
            foreach (var planet in redPlanets)
            {
                float temp1 = planet.transform.position.x - player.position.x;
                float temp2 = planet.transform.position.z - player.position.z;
                if (temp1 < 0)
                {
                    temp1 *= -1;
                }

                if (temp2 < 0)
                {
                    temp2 *= -1;
                }

                if (distance > temp1 + temp2)
                {
                    distance = temp1 + temp2;
                    target = planet.transform;
                    distanceX = player.position.x - target.transform.position.x;
                    distanceZ = player.position.z - target.transform.position.z;
                }
            }
        }

        else if (gameObject.name == "GreenPointerCircle")
        {
            var greenPlanets = GameObject.FindGameObjectsWithTag("GreenPlanet");
            foreach (var planet in greenPlanets)
            {
                float temp1 = planet.transform.position.x - player.position.x;
                float temp2 = planet.transform.position.z - player.position.z;
                if (temp1 < 0)
                {
                    temp1 *= -1;
                }

                if (temp2 < 0)
                {
                    temp2 *= -1;
                }

                if (distance > temp1 + temp2)
                {
                    distance = temp1 + temp2;
                    target = planet.transform;
                    distanceX = player.position.x - target.transform.position.x;
                    distanceZ = player.position.z - target.transform.position.z;
                }
            }
        }

        else if (gameObject.name == "YellowPointerCircle")
        {
            var greenPlanets = GameObject.FindGameObjectsWithTag("GreenPlanet");
            foreach (var planet in greenPlanets)
            {
                if (planet.GetComponent<Capture>().isBeingCaptured == true)
                {
                    float temp1 = planet.transform.position.x - player.position.x;
                    float temp2 = planet.transform.position.z - player.position.z;
                    if (temp1 < 0)
                    {
                        temp1 *= -1;
                    }

                    if (temp2 < 0)
                    {
                        temp2 *= -1;
                    }

                    if (distance > temp1 + temp2)
                    {
                        distance = temp1 + temp2;
                        target = planet.transform;
                        distanceX = player.position.x - target.transform.position.x;
                        distanceZ = player.position.z - target.transform.position.z;
                    }
                }
            }
        }

        if (distance < 9999999f)
        {
            gameObject.transform.forward = new Vector3(distanceX, 0, distanceZ);
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }

        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
        
    }
}
