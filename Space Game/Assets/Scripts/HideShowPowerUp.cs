using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HideShowPowerUp : MonoBehaviour
{

    public Animator anim;
    static public bool isOverUI;

    public void HidePowerUp()
    {
        if (anim.GetBool("Hidden") == false)
        {
            anim.SetBool("Hidden", true);
        }

        else
        {
            anim.SetBool("Hidden", false);
        }
    }

    private bool IsMouseOver()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<MouseUIClickthrough>() != null)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }

        return raycastResultList.Count > 0;
    }

    private void Update()
    {
        isOverUI = IsMouseOver();
    }
}
