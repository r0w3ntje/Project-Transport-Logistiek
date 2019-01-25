using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TransportLogistiek
{
    public class MachineProduction : MonoBehaviour
    {
        public MachineStateEnum machineState;

        [HideInInspector] public float productionTimer;
        private bool finishedProducing = true;

        private MachineUpgrade machineUpgrade;

        private void Awake()
        {
            machineUpgrade = GetComponent<MachineUpgrade>();

            MachineStateChangeEvent.OnMachineStateChange += StartProduction;
        }

        private void Start()
        {
            productionTimer = CurrentUpgrade().producingTime;
        }

        private void Update()
        {
            Production();
        }

        public MachineUpgrade.MachineUpgrades CurrentUpgrade()
        {
            return machineUpgrade.upgrades[machineUpgrade.machineLevel];
        }

        private bool HasSufficientInputUnits()
        {
            bool hasEnoughUnits = false;

            if (CurrentUpgrade().unitInput.Length != 0)
            {
                for (int i = 0; i < CurrentUpgrade().unitInput.Length; i++)
                {
                    if (CurrentUpgrade().unitInput[i].unit != UnitEnum.None)
                    {
                        hasEnoughUnits = PlayerData.Instance().HasSufficientUnits(CurrentUpgrade().unitInput[i].unit, CurrentUpgrade().unitInput[i].amount);
                    }
                }
            }
            else hasEnoughUnits = true;

            return hasEnoughUnits;
        }

        public void StartProduction()
        {
            bool canProduce = HasSufficientInputUnits();

            if (canProduce == false)
                return;

            if (finishedProducing == false)
                return;

            if (CurrentUpgrade().unitInput.Length != 0)
            {
                for (int i = 0; i < CurrentUpgrade().unitInput.Length; i++)
                {
                    if (CurrentUpgrade().unitInput[i].unit != UnitEnum.None)
                    {
                        PlayerData.Instance().Add(CurrentUpgrade().unitInput[i].unit, -CurrentUpgrade().unitInput[i].amount);
                    }
                }
            }

            Debug.Log("New Production");
            finishedProducing = false;
            productionTimer = CurrentUpgrade().producingTime;
            MachineMenu.Instance().SetData(MachineMenu.Instance().machine);
        }

        private void Production()
        {
            if (machineState == MachineStateEnum.On)
            {
                if (productionTimer >= 0f && finishedProducing == false)
                {
                    if (PlayerData.Instance().unitData[UnitEnum.Energy] >= 0f)
                    {
                        PlayerData.Instance().Add(UnitEnum.Energy, -CurrentUpgrade().energyConsumptionPerSec * Time.deltaTime);
                        productionTimer -= Time.deltaTime;
                    }

                    if (CurrentUpgrade().energyConsumptionPerSec == 0f)
                        productionTimer -= Time.deltaTime;
                }

                if (productionTimer <= 0f)
                    FinishedProduction();
            }
        }

        private void FinishedProduction()
        {
            if (finishedProducing == false)
            {
                // Add resources
                if (CurrentUpgrade().unitOutput.Length != 0)
                {
                    for (int i = 0; i < CurrentUpgrade().unitOutput.Length; i++)
                    {
                        if (CurrentUpgrade().unitOutput[i].unit != UnitEnum.None)
                        {
                            PlayerData.Instance().Add(CurrentUpgrade().unitOutput[i].unit, CurrentUpgrade().unitOutput[i].amount);
                        }
                    }
                }

                MachineStateChangeEvent.CallEvent();
                finishedProducing = true;
            }

            StartProduction();
        }
    }
}