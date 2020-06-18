using Unity.Mathematics;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public int regenSpeed;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Health>().anim;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = GetComponent<Health>().currentHealth;
        maxHealth = GetComponent<Health>().maxHealth;

        if (currentHealth < maxHealth)
        {
            if (gameObject.tag == "Ally")
            {
                var planetList = GameObject.FindGameObjectsWithTag("GreenPlanet");
                foreach (var planet in planetList)
                {
                    if (planet.transform.position.x + 3f > gameObject.transform.position.x && gameObject.transform.position.x  > planet.transform.position.x - 3f)
                    {
                        if (planet.transform.position.z + 3f > gameObject.transform.position.z && gameObject.transform.position.z > planet.transform.position.z - 3f)
                        {
                            currentHealth += regenSpeed;
                            GetComponent<Health>().currentHealth = currentHealth;
                            anim.SetTrigger("Hit");
                        }
                    }
                }
            }

            else if (gameObject.tag == "Enemy")
            {
                var planetList = GameObject.FindGameObjectsWithTag("RedPlanet");
                foreach (var planet in planetList)
                {
                    if (planet.transform.position.x + 3f > gameObject.transform.position.x && gameObject.transform.position.x > planet.transform.position.x - 3f)
                    {
                        if (planet.transform.position.z + 3f > gameObject.transform.position.z && gameObject.transform.position.z > planet.transform.position.z - 3f)
                        {
                            currentHealth += regenSpeed;
                            GetComponent<Health>().currentHealth = currentHealth;
                            anim.SetTrigger("Hit");
                        }
                    }
                }
            }
        }
    }
}
