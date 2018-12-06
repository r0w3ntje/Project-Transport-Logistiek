using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TransportLogistiek
{
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
            ironUpgradeCosts--;
            PlayerData.Instance().Add(ref PlayerData.Instance().iron, -1);

            if (ironUpgradeCosts <= 0)
            {
                machineLevel++;

                UpdateMachineStats();

                GetComponent<Machine>().SetText();
            }
            else UpdateTexts();
        }

        private void UpdateMachineStats()
        {
            ironUpgradeCosts = Mathf.RoundToInt(machineLevel * machineLevel * costsIncreaseFactor);
            amountPerProducing = machineLevel;

            ResetProductionTimer();

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            upgradeText.text = "Je hebt " + ironUpgradeCosts + " Ijzeren kratten nodig om de machine te verbeteren.\nVerbeter '" + PlayerInteraction.Instance().upgradeKeyBind + "'";
        }

        private void ResetProductionTimer()
        {
            var a = 11f - machineLevel;
            if (a < 2f) a = 2f;

            producingTime = a;
        }
    }
}