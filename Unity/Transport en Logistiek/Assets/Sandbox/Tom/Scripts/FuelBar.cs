using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public float fuelInt;     //the int for the amount the fuel will go down

    private float time;
    private Image fuelImage;

    void Start()
    {
        fuelImage = GetComponent<Image>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time >= 0.01f)
        {
            fuelImage.fillAmount = fuelImage.fillAmount - FuelCalc(fuelInt);
            time = 0;
            fuelImage.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
    }

    public float FuelCalc(float newInt)
    {
        float fuel = fuelInt / 100000;
        return fuel;
    }
}
