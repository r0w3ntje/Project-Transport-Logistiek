using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TransportLogistiek
{
    public class MachineProduction : MonoBehaviour
    {
        [Header("Production")]
        public bool isOn;

        [Space(8)]

        public UnitEnum unitInput;
        public UnitEnum unitOutput;

        private float productionTimer;

        private Machine machine;
        private MachineUpgrade machineUpgrade;

        private void Start()
        {
            machine = GetComponent<Machine>();
            machineUpgrade = GetComponent<MachineUpgrade>();

            productionTimer = machineUpgrade.upgrades[machineUpgrade.machineLevel].producingTime;
        }

        private void Update()
        {
            ProductionProcess();

            //Debug.Log("tIMER: " + productionTimer);
        }

        public void StartNewProduction()
        {
            if (unitInput != UnitEnum.Geen)
            {
                if (PlayerData.Instance().HasSufficientUnits(unitInput, machineUpgrade.upgrades[machineUpgrade.machineLevel].unitInputAmount) == false)
                {
                    return;
                }
            }

            productionTimer = machineUpgrade.upgrades[machineUpgrade.machineLevel].producingTime;
            machine.AddUnits(unitOutput, machineUpgrade.upgrades[machineUpgrade.machineLevel].unitOutputAmount);


            Debug.Log("New Production");

            MachineMenu.Instance().SetData(MachineMenu.Instance().machine);
        }

        // The production process progress
        private void ProductionProcess()
        {
            if (isOn)
            {
                if (PlayerData.Instance().energy >= 0f)
                {
                    productionTimer -= Time.deltaTime;
                    PlayerData.Instance().Add(ref PlayerData.Instance().energy, -machineUpgrade.upgrades[machineUpgrade.machineLevel].energyConsumptionPerSec * Time.deltaTime);
                }

                if (productionTimer <= 0f)
                {
                    StartNewProduction();
                }

            }
        }
    }
}
