using UnityEngine;

public class Drag : MonoBehaviour
{
    public Animator anim;
    public GameObject powerUpSlot;
    public GameObject insertImage;
    public GameObject deleteImage;
    public int powerUpID;
    public int maxUses;
    bool notHidden;
    bool isOver;

    private void Start()
    {
        notHidden = false;
        isOver = false;
        anim = GameObject.Find("PowerUpSlot").GetComponent<Animator>();
        powerUpSlot = GameObject.Find("PowerUpSlot");
        insertImage = powerUpSlot.transform.Find("InsertPowerUp").gameObject;
        deleteImage = powerUpSlot.transform.Find("DeletePowerUp").gameObject;
    }

    private void OnMouseDrag()
    {
        GameManager.powerupHeld = true;
        Vector3 powerUpPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        powerUpPosition.y = 0f;
        transform.position = powerUpPosition;
        if (Input.mousePosition.x <= 110 && Input.mousePosition.y >= 420)
        {
            if (anim.GetBool("Hidden") == false)
            {
                if (powerUpSlot.GetComponent<PowerUps>().powerUpID == 0)
                {
                    if (deleteImage.activeSelf == true)
                    {
                        deleteImage.SetActive(false);
                    }

                    if (insertImage.activeSelf == false)
                    {
                        insertImage.SetActive(true);
                    }
                    isOver = true;
                }

                if (powerUpSlot.GetComponent<PowerUps>().powerUpID != 0)
                {
                    if (insertImage.activeSelf == true)
                    {
                        insertImage.SetActive(false);
                    }

                    if (deleteImage.activeSelf == false)
                    {
                        deleteImage.SetActive(true);
                    }
                    isOver = true;
                }
            }
            else if (Input.mousePosition.x <= 34 && Input.mousePosition.y >= 420)
            {
                anim.SetBool("Hidden", false);
                notHidden = true;
            }
        }
        else
        {
            if (insertImage.activeSelf == true)
            {
                insertImage.SetActive(false);
            }

            if (deleteImage.activeSelf == true)
            {
                deleteImage.SetActive(false);
            }

            if (notHidden)
            {
                anim.SetBool("Hidden", true);
                notHidden = false;
            }
            isOver = false;
        }
    }

    private void OnMouseUp()
    {
        GameManager.powerupHeld = false;
        if (insertImage.activeSelf == true)
        {
            insertImage.SetActive(false);
        }

        if (deleteImage.activeSelf == true)
        {
            deleteImage.SetActive(false);
        }

        if (notHidden)
        {
            anim.SetBool("Hidden", true);
            notHidden = false;
        }

        if (isOver)
        {
            powerUpSlot.GetComponent<PowerUps>().powerUpID = powerUpID;
            powerUpSlot.GetComponent<PowerUps>().powerUpImage.sprite = powerUpSlot.GetComponent<PowerUps>().powerUpImages[powerUpID];
            powerUpSlot.GetComponent<PowerUps>().maxUses = maxUses;
            powerUpSlot.GetComponent<PowerUps>().uses = maxUses;
            isOver = false;
            Destroy(gameObject);
        }
    }
}
