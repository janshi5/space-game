using System;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    ParticleSystem boost;
    NavMeshAgent agent;
    public Transform target;
    float energy;
    int followerCount;
    bool followGeneral;
    public Transform aim;
    string enemy;
    string ally;
    string enemyPlanet;
    string allyPlanet;
    bool healing;
    bool helping;
    float distance;
    public float visionRange;
    Gradient defGradient;
    Gradient boostGradient;
    public Transform call;

    void Start()
    {
        call = null;
        followerCount = 0;
        followGeneral = false;
        boost = gameObject.GetComponentInChildren<ParticleSystem>();
        defGradient = new Gradient();
        defGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(1, 0.494f, 0.494f), 0.047f), new GradientColorKey(new Color(0.592f, 0.08f, 0.08f), 0.347f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.447f), new GradientAlphaKey(0.3608f, 1.0f) });
        boostGradient = new Gradient();
        boostGradient.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0.494f, 0.494f, 1), 0.047f), new GradientColorKey(new Color(0.08f, 0.08f, 0.592f), 0.347f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.447f), new GradientAlphaKey(0.3608f, 1.0f) });
        healing = false;
        helping = false;
        agent = gameObject.GetComponent<NavMeshAgent>();
        if (gameObject.tag == "Ally")
        {
            enemy = "Enemy";
            ally = "Ally";
            allyPlanet = "GreenPlanet";
            enemyPlanet = "RedPlanet";
        }

        else if (gameObject.tag == "Enemy")
        {
            enemy = "Ally";
            ally = "Enemy";
            allyPlanet = "RedPlanet";
            enemyPlanet = "GreenPlanet";
        }
    }

    // Update is called once per frame
    void Update()
    {
        var col = boost.colorOverLifetime;
        col.color = defGradient;
        energy = aim.GetComponent<Fire>().currentEnergy;
        (target, distance) = GetEnemy(enemy);
        aim.GetComponent<Aim>().target = target;
        aim.GetComponent<Fire>().targetDistance = distance;

        if (helping == true)
        {
            if (agent.remainingDistance < 2f)
            {
                helping = false;
            }
        }

        else if (call != null)
        {
            agent.stoppingDistance = 0f;
            agent.SetDestination(call.position);
            if (agent.speed != 5)
            {
                agent.speed = 5;
            }
            call = null;
            helping = true;
        }

        else if (healing == true)
        {
            if (agent.remainingDistance >= 1 && energy >= 5)
            {
                Boost();
            }

            if (gameObject.GetComponent<Health>().currentHealth >= gameObject.GetComponent<Health>().maxHealth)
            {
                healing = false;
                agent.stoppingDistance = 4f;
            }

            else
            {
                if (agent.speed != 5)
                {
                    agent.speed = 5;
                }
            }
        }

        else if (gameObject.GetComponent<Health>().currentHealth <= gameObject.GetComponent<Health>().maxHealth / 5)
        {
            (target, distance) = GetClosestPlanet(allyPlanet);
            agent.SetDestination(target.position);
            agent.stoppingDistance = 0f;
            if (agent.speed != 5)
            {
                agent.speed = 5;
            }
            healing = true;
        }

        else if (gameObject.GetComponent<Health>().anger >= 3 && GetPlayer().Item2 <= visionRange)
        {
            (target, distance) = GetPlayer();
            if (agent.remainingDistance *2 < distance)
            {
                agent.SetDestination(target.position + (target.GetComponent<Rigidbody>().velocity) * 2);
            }
            agent.stoppingDistance = 4f;
            aim.GetComponent<Aim>().target = target;
            aim.GetComponent<Fire>().targetDistance = distance;

            if (agent.speed != 5)
            {
                agent.speed = 5;
            }
        }

        else if (distance <= visionRange)
        {
            if (agent.remainingDistance*2 < distance)
            {
                agent.SetDestination(target.position + (target.GetComponent<Rigidbody>().velocity) * 2);
            }
            agent.stoppingDistance = 4f;

            if (agent.speed != 5)
            {
                agent.speed = 5;
            }
        }


        else if (GetClosestPlanet(enemyPlanet).Item2 <= visionRange)
        {
            var planetPosition = GetClosestPlanet(enemyPlanet).Item1.position;
            if (agent.destination != planetPosition)
            {
                agent.SetDestination(planetPosition);
            }
            agent.stoppingDistance = 0f;

            if (agent.speed != 5)
            {
                agent.speed = 5;
            }
        }

        else if (followerCount >= 5)
        {
            var planetPosition = GetClosestPlanet(enemyPlanet).Item1.position;
            if (agent.destination != planetPosition)
            {
                agent.SetDestination(planetPosition);
            }
            agent.stoppingDistance = 0f;

            if (agent.speed != 5)
            {
                agent.speed = 5;
            }
        }

        else if (ally == "Ally" && GetPlayer().Item2 <= visionRange && gameObject.name != "GeneralGreen")
        {
            if (agent.remainingDistance*2 < GetPlayer().Item2)
            {
                agent.SetDestination(GameObject.Find("Player").transform.position + (GameObject.Find("Player").GetComponent<Rigidbody>().velocity)*2);
            }
            agent.stoppingDistance = 4f;

            if (Input.GetKey(KeyCode.LeftShift) && energy >= 5)
            {
                Boost();
            }

            else if (agent.speed != 5)
            {
                agent.speed = 5;
            }
        }

        else if (GetGeneral(ally).Item2 <= visionRange && gameObject.name != "GeneralGreen" && gameObject.name != "GeneralRed")
        {
            if (agent.remainingDistance * 2 < GetGeneral(ally).Item2)
            {
                if (ally == "Ally")
                {
                    agent.SetDestination(GameObject.Find("GeneralGreen").transform.position + (GameObject.Find("GeneralGreen").GetComponent<Rigidbody>().velocity) * 2);

                    if (followGeneral == false)
                    {
                        GameObject.Find("GeneralGreen").GetComponent<AI_Movement>().followerCount += 1;
                        followGeneral = true;
                    }
                }

                if (ally == "Enemy")
                {
                    agent.SetDestination(GameObject.Find("GeneralRed").transform.position + (GameObject.Find("GeneralRed").GetComponent<Rigidbody>().velocity) * 2);

                    if (followGeneral == false)
                    {
                        GameObject.Find("GeneralRed").GetComponent<AI_Movement>().followerCount += 1;
                        followGeneral = true;
                    }
                }
            }
            agent.stoppingDistance = 4f;
        }

        else if (IsUndrAttack(allyPlanet).Item2 == true)
        {
            var planetPosition = IsUndrAttack(allyPlanet).Item1.position;
            if (agent.destination != planetPosition)
            {
                agent.SetDestination(planetPosition);
            }

            if (agent.speed != 5)
            {
                agent.speed = 5;
            }
        }

        else if (agent.remainingDistance <= 4)
        {
            Vector3 randomDestination = new Vector3(transform.position.x + UnityEngine.Random.Range(-30,30), 0f, transform.position.z + UnityEngine.Random.Range(-30, 30));
            agent.SetDestination(randomDestination);
            agent.stoppingDistance = 1f;
            if (agent.speed != 3.5f)
            {
                agent.speed = 3.5f;
            }
        }
    }

    private Tuple <Transform, float> GetEnemy(string tag)
    {
        float distance = 999999f;
        var targetList = GameObject.FindGameObjectsWithTag(tag);
        foreach (var newTarget in targetList)
        {
            float temp1 = newTarget.transform.position.x - gameObject.transform.position.x;
            float temp2 = newTarget.transform.position.z - gameObject.transform.position.z;
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
                target = newTarget.transform;
            }
        }
        return Tuple.Create(target, distance);
    }

    private Tuple<Transform, float> GetClosestPlanet(string tag)
    {
        float distance = 99999999;
        var planets = GameObject.FindGameObjectsWithTag(tag);
        foreach (var planet in planets)
        {
            float temp1 = planet.transform.position.x - gameObject.transform.position.x;
            float temp2 = planet.transform.position.z - gameObject.transform.position.z;
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
            }
        }
        return Tuple.Create(target, distance);
    }

    private Tuple<Transform, bool> IsUndrAttack(string tag)
    {
        distance = 100;
        var planets = GameObject.FindGameObjectsWithTag(tag);
        bool isNear = false;
        foreach (var planet in planets)
        {
            if (planet.GetComponent<Capture>().isBeingCaptured == true)
            {
                float temp1 = planet.transform.position.x - gameObject.transform.position.x;
                float temp2 = planet.transform.position.z - gameObject.transform.position.z;
                if (temp1 < 0)
                {
                    temp1 *= -1;
                }

                if (temp2 < 0)
                {
                    temp2 *= -1;
                }

                if (distance >= temp1 + temp2)
                {
                    distance = temp1 + temp2;
                    target = planet.transform;
                    isNear = true;
                }
            }

        }
        return Tuple.Create(target, isNear);
    }

    private Tuple<Transform, float> GetPlayer()
    {
        float distance;
        target = GameObject.Find("Player").transform;
        float temp1 = gameObject.transform.position.x - target.position.x;
        float temp2 = gameObject.transform.position.z - target.position.z;
        if (temp1 < 0)
        {
            temp1 *= -1;
        }

        if (temp2 < 0)
        {
            temp2 *= -1;
        }
        distance = temp1 + temp2;
        return Tuple.Create(target, distance);
    }

    private Tuple<Transform, float> GetGeneral(string tag)
    {
        float distance;
        if (tag == "Ally" && GameManager.generalGreen == true)
        {
            target = GameObject.Find("GeneralGreen").transform;
        }

        else if (tag == "Enemy" && GameManager.generalRed == true)
        {
            target = GameObject.Find("GeneralRed").transform;
        }

        else
        {
            return Tuple.Create(gameObject.transform , 99999999f);
        }

        float temp1 = gameObject.transform.position.x - target.position.x;
        float temp2 = gameObject.transform.position.z - target.position.z;
        if (temp1 < 0)
        {
            temp1 *= -1;
        }

        if (temp2 < 0)
        {
            temp2 *= -1;
        }
        distance = temp1 + temp2;
        return Tuple.Create(target, distance);
    }

    private void Boost()
    {
        if (agent.speed != 7.5)
        {
            agent.speed = 7.5f;
        }
        var col = boost.colorOverLifetime;
        col.color = boostGradient;
        aim.GetComponent<Fire>().currentEnergy -= Time.deltaTime * 30f;
        if (energy <= 6)
        {
            aim.GetComponent<Fire>().currentEnergy -= 5;
        }
    }
}
