using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class MachineUpgrade : MonoBehaviour {

    int currentXP = 0;
    int nextLevelUp = 5;
    int currentLevel = 0;


    int amountOfIron = 0;

    // Use this for initialization
    void Start () {
        StartCor();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
            Debug.Log("The machine gained 1 iron. The machine has " + amountOfIron + " iron.");
            StartCor();


        }

    }

    // Update is called once per frame
    void Update () {


    }

    // als de machine collide met iron om te upgraden
    // gainExperience();
    // then destroy iron die je geeft
    // behalve als lvl hoger is dan X
    public void OnTriggerEnter(Collider other)
    {
        if (currentLevel <= 7)
        {
            if (other.gameObject.tag == "iron")
            {
                GainExperience();
                Destroy(other.gameObject);
            }
        } else
        {
            Debug.Log("The mining machine has reached the maximum level.");
        }
    }

    void GainExperience()
    {
        currentXP++;
        Debug.Log("The mining machine has " + currentXP +" XP.");
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
