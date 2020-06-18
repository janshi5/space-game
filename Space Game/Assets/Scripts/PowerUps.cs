using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    public int powerUpID;
    public int uses;
    public int maxUses;
    public Sprite[] powerUpImages;
    public Image powerUpImage;
    public GameObject powerUpObject;
    public Transform player;
    public Slider useSlider;

    // Start is called before the first frame update
    void Start()
    {
        powerUpID = 0;
        uses = 0;
        maxUses = 0;
        if (powerUpObject.activeSelf == true)
        {
            powerUpObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        useSlider.maxValue = maxUses;
        useSlider.value = uses;

        if (uses == 0)
        {
            powerUpID = 0;
            if (powerUpObject.activeSelf == true)
            {
                powerUpObject.SetActive(false);
            }
        }
        else
        {
            if (powerUpObject.activeSelf == false)
            {
                powerUpObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && powerUpID != 0)
        {
            UsePowerUp();
            uses -= 1;
        }
    }

    void UsePowerUp()
    {
        if (powerUpID == 1)
        {
            CallForHelp("Ally");
        }

        else if (powerUpID == 2)
        {
            CallForHelp("Enemy");
        }
    }

    void CallForHelp(string tag)
    {
        float distance = 100f;
        var targetList = GameObject.FindGameObjectsWithTag(tag);
        foreach (var newTarget in targetList)
        {
            float temp1 = newTarget.transform.position.x - player.transform.position.x;
            float temp2 = newTarget.transform.position.z - player.transform.position.z;
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
                if (newTarget.name != "Player")
                {
                    newTarget.GetComponent<AI_Movement>().call = player;
                }
            }
        }
    }
}
