using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class MachineUpgrade : MonoBehaviour
{
    private int currentXP = 0;
    private int nextLevelUp = 2;
    private int currentLevel = 0;

    public float producingTime = 10f;

    //private int amount = 0;

    private Machine machine;

    private void Start()
    {
        machine = GetComponent<Machine>();
    }

    // als de machine collide met iron om te upgraden
    // gainExperience();
    // then destroy iron
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "iron")
    //    {
    //        GainExperience();
    //        Destroy(other.gameObject);
    //    }
    //}

    //public void AddUnits()
    //{
    //amount++;

    //Debug.Log("You gained 1 " + machine.producedUnit + ". You now have " + amount + " iron.");

    //machineTimer = 10f - currentLevel;
    //if (machineTimer < 2f) machineTimer = 2f;

    //GainExperience();
    //}

    public void GainExperience()
    {
        currentXP++;

        //Debug.Log("You now have " + currentXP + " XP for this machine.");

        if (currentXP >= nextLevelUp)
        {
            LevelUp();
        }

        SetMachineTimer();
    }

    private void SetMachineTimer()
    {
        var a = 10f - currentLevel;
        if (a < 2f) a = 2f;

        producingTime = a;
    }

    // als je in het volgende level bent
    // current level gaat omhoog
    // meer XP nodig voor volgende nextLevelUp
    // machineTimer word gereset naar machineTimer - currentLevel;
    private void LevelUp()
    {
        currentLevel++;

        nextLevelUp += nextLevelUp;

        //Debug.Log("You are now level " + currentLevel + ".");
    }
}