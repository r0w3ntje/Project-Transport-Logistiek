using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TransportLogistiek
{
    public class MachineUpgrade : MonoBehaviour
    {
        [Header("Machine Upgrade")]
        public int machineLevel;

        [Header("Upgrades")]
        public List<MachineUpgrades> upgrades;

        private Machine machine;

        private void Awake()
        {
            Load();
        }

        private void Start()
        {
            machine = GetComponent<Machine>();
        }

        private void Load()
        {
            if (machine == null) Start();
            machineLevel = PlayerData.Instance().LoadInt(machine.uniqueID);
        }
        private void Save()
        {
            PlayerData.Instance().SaveInt(machine.uniqueID, machineLevel);
        }

        public void Upgrade()
        {
            if (machineLevel >= upgrades.Count - 1)
            {
                return;
            }

            if (PlayerData.Instance().HasSufficientUnits(UnitEnum.Ijzer, upgrades[machineLevel].ironUpgradeCosts))
            {
                machineLevel++;
                Save();
            }

            MachineMenu.Instance().SetData(MachineMenu.Instance().machine);
        }

        [System.Serializable]
        public class MachineUpgrades
        {
            [Header("Production")]
            public int unitInputAmount;
            public int unitOutputAmount;

            [Space(8)]

            public float energyConsumptionPerSec;
            public float producingTime;

            [Space(8)]

            public int ironUpgradeCosts;
        }

        //[SerializeField] private int machineLevel;

        //[Space(8)]

        //public int amountPerProducing;
        //public int ironUpgradeCosts;
        //[SerializeField] private float costsIncreaseFactor = 2f;

        //[Space(8)]

        //public float producingTime;

        //[Space(8)]

        //public Text upgradeText;

        //private void Start()
        //{
        //    machineLevel = 1;

        //    UpdateMachineStats();
        //}

        //public void Upgrade()
        //{
        //    ironUpgradeCosts--;
        //    PlayerData.Instance().Add(ref PlayerData.Instance().iron, -1);

        //    if (ironUpgradeCosts <= 0)
        //    {
        //        machineLevel++;

        //        UpdateMachineStats();

        //        //GetComponent<Machine>().SetText();
        //    }
        //    else UpdateTexts();
        //}

        //private void UpdateMachineStats()
        //{
        //    ironUpgradeCosts = Mathf.RoundToInt(machineLevel * machineLevel * costsIncreaseFactor);
        //    amountPerProducing = machineLevel;

        //    ResetProductionTimer();

        //    UpdateTexts();
        //}

        //private void UpdateTexts()
        //{
        //    upgradeText.text = "Je hebt " + ironUpgradeCosts + " " + (ironUpgradeCosts == 0 ? "Ijzer krat" : "Ijzeren kratten ") + "nodig om de machine te verbeteren.\nVerbeter '" + PlayerInteraction.Instance().upgradeKeyBind + "'";
        //}

        //private void ResetProductionTimer()
        //{
        //    var a = 11f - machineLevel;
        //    if (a < 2f) a = 2f;

        //    producingTime = a;
        //}
    }
}