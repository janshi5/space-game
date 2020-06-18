using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public GameObject aimCircle;

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = aimCircle.GetComponent<PlayerFire>().maxEnergy;
        slider.value = aimCircle.GetComponent<PlayerFire>().currentEnergy;
    }
}
