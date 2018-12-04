using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class MachineUpgrade : MonoBehaviour {

    int currentXP = 0;
    int nextLevelUp = 5;
    int currentLevel = 0;

    float machineTimer = 10f;

    int amountOfIron = 0;

    // Use this for initialization
    void Start () {
        StartCor();

    }

    void StartCor()
    {
        StartCoroutine("Countdown", 10 - currentLevel);
    }

   private IEnumerator Countdown(int time)
    {
        while (time >= 0)
        {
            //Debug.Log(time--);    //timer test
            yield return new WaitForSeconds(1);
        }
        if (time <= 0)
        {
            amountOfIron++;
            Debug.Log("You gained 1 iron. You now have " + amountOfIron + " iron.");
            StartCor();


        }

    }

    // Update is called once per frame
    void Update () {
       /*machineTimer = Time.deltaTime;
        if (machineTimer < 0)
        {
            amountOfIron++;
            Debug.Log("You gained 1 iron.");
        }*/


    }

    // als de machine collide met iron om te upgraden
    // gainExperience();
    // then destroy iron
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "iron")
        {
            GainExperience();
            Destroy(other.gameObject);
            Debug.Log("Hello poops");
        }
    }

    void GainExperience()
    {
        currentXP++;
        Debug.Log("You now have " + currentXP +" XP.");
        if (currentXP >= nextLevelUp)
        {
            levelUp();
        }
    }

    // als je in het volgende level bent
    // current level gaat omhoog
    // meer XP nodig voor volgende nextLevelUp
    // machineTimer word gereset naar machineTimer - currentLevel;
    void levelUp()
    {
        currentLevel++;

        nextLevelUp += nextLevelUp + 5;
       // machineTimer -= 10f - currentLevel;
        Debug.Log("You are now level " + currentLevel + ".");
    }

}
