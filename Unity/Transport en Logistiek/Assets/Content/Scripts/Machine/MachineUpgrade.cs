using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineUpgrade : MonoBehaviour
{
    [SerializeField] private int machineLevel;

    [Space(8)]

    public int amountPerProducing;
    public int ironUpgradeCosts;
    [SerializeField] private float costsIncreaseFactor = 2f;

    [Space(8)]

    public float producingTime;

    [Space(8)]

    public Text upgradeText;

    private void Start()
    {
        machineLevel = 1;

        UpdateMachineStats();
    }

    public void Upgrade()
    {
        if (PlayerData.Instance().iron >= ironUpgradeCosts)
        {
            PlayerData.Instance().Add(ref PlayerData.Instance().iron, -ironUpgradeCosts);

            machineLevel++;

            UpdateMachineStats();

            GetComponent<Machine>().SetText();
        }
    }

    private void UpdateMachineStats()
    {
        ironUpgradeCosts = Mathf.RoundToInt(machineLevel * machineLevel * costsIncreaseFactor);
        amountPerProducing = machineLevel;

        SetProductionTimer();

        UpdateTexts();
    }

    private void UpdateTexts()
    {
        upgradeText.text = "Je hebt " + ironUpgradeCosts + " Ijzer met een ijzer krat nodig om de machine te verbeteren.\nVerbeter '" + PlayerInteraction.Instance().upgradeKeyBind + "'";
    }

    private void SetProductionTimer()
    {
        var a = 11f - machineLevel;
        if (a < 2f) a = 2f;

        producingTime = a;
    }
}