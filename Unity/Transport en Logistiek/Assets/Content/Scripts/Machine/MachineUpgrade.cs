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

        private void Start()
        {
            machine = GetComponent<Machine>();
            Load();
        }

        private void Load()
        {
            machineLevel = PlayerData.Instance().LoadInt(machine.uniqueID);
        }
        private void Save()
        {
            PlayerData.Instance().SaveInt(machine.uniqueID, machineLevel);
        }

        public void Upgrade()
        {
            if (PlayerData.Instance().HasSufficientUnits(UnitEnum.Ijzer, upgrades[machineLevel].ironUpgradeCosts))
            {
                machineLevel++;
                if (machineLevel >= upgrades.Count - 1)
                {
                    machineLevel = upgrades.Count - 1;
                    return;
                }
                Save();

                PlayerData.Instance().Add(ref PlayerData.Instance().iron, -upgrades[machineLevel - 1].ironUpgradeCosts);
                MachineMenu.Instance().SetData(MachineMenu.Instance().machine);
            }
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
    }
}