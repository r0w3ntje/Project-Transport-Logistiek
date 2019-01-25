using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TransportLogistiek
{
    public class MachineProduction : MonoBehaviour
    {
        [Header("Machine Production")]
        public MachineStateEnum machineState;

        [HideInInspector] public float productionTimer;

        private MachineUpgrade machineUpgrade;

        private void Awake()
        {
            machineUpgrade = GetComponent<MachineUpgrade>();
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

        public void StartProduction()
        {
            bool canProduce = true;

            if (CurrentUpgrade().unitInput.Length != 0)
            {
                for (int i = 0; i < CurrentUpgrade().unitInput.Length; i++)
                {
                    if (CurrentUpgrade().unitInput[i].unit != UnitEnum.None)
                    {
                        // If there's enough input resources to start production
                        canProduce = PlayerData.Instance().HasSufficientUnits(CurrentUpgrade().unitInput[i].unit,
                            CurrentUpgrade().unitInput[i].amount);
                    }
                }
            }

            if (canProduce == false)
            {
                return;
            }

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

            productionTimer = CurrentUpgrade().producingTime;

            Debug.Log("New Production");

            MachineMenu.Instance().SetData(MachineMenu.Instance().machine);
        }

        private void Production()
        {
            if (machineState == MachineStateEnum.On)
            {
                if (PlayerData.Instance().unitData[UnitEnum.Energy] >= 0f)
                {
                    PlayerData.Instance().Add(UnitEnum.Energy, -CurrentUpgrade().energyConsumptionPerSec * Time.deltaTime);
                    productionTimer -= Time.deltaTime;
                }

                if (CurrentUpgrade().energyConsumptionPerSec == 0f)
                {
                    productionTimer -= Time.deltaTime;
                }

                if (productionTimer <= 0f)
                {
                    FinishedProduction();
                }
            }
        }

        private void FinishedProduction()
        {
            // Add resources
            if (CurrentUpgrade().unitOutput.Length != 0)
            {
                for (int i = 0; i < CurrentUpgrade().unitOutput.Length; i++)
                {
                    if (CurrentUpgrade().unitOutput[i].unit != UnitEnum.None)
                    {
                        PlayerData.Instance().Add(CurrentUpgrade().unitOutput[i].unit,
                            CurrentUpgrade().unitOutput[i].amount);
                    }
                }
            }

            productionTimer = CurrentUpgrade().producingTime;

            StartProduction();
        }
    }
}