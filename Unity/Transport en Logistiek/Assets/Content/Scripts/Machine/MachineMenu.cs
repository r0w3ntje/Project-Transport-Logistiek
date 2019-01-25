using System.Collections;
using System.Collections.Generic;
using Systems.Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace TransportLogistiek
{
    public class MachineMenu : Singleton<MachineMenu>
    {
        [Header("UserInterface")]
        [SerializeField] private GameObject menuPanel;

        [SerializeField] private Text nameText;

        [Header("Toggle")]
        [SerializeField] private Toggle machineStateToggle;
        [SerializeField] private Slider progressBar;

        [Header("Upgrade")]
        [SerializeField] private Text upgradeInfoText;
        [SerializeField] private Image upgradeButtonImage;

        [Header("Info")]
        [SerializeField] private Text infoText;

        [Header("Colors")]
        [SerializeField] private Color active;
        [SerializeField] private Color inActive;

        [HideInInspector] public Machine machine;

        private void Start()
        {
            Close();
        }

        private void Update()
        {
            if (machine != null)
                progressBar.value = 1f - (machine.machineProduction.productionTimer / machine.machineProduction.CurrentUpgrade().producingTime);
        }

        public void Open(Machine _machine)
        {
            SetData(_machine);
            menuPanel.SetActive(true);
        }

        public void Close()
        {
            menuPanel.SetActive(false);
        }

        public void SetData(Machine _machine)
        {
            machine = _machine;

            machineStateToggle.isOn = machine.machineProduction.machineState == MachineStateEnum.On;
            UpdateTexts();
        }

        public void MachineState()
        {
            machine.machineProduction.machineState = machineStateToggle.isOn ? MachineStateEnum.On : MachineStateEnum.Off;

            MachineStateChangeEvent.CallEvent();
        }

        public void Upgrade()
        {
            machine.machineUpgrade.Upgrade();
        }

        private MachineUpgrade.MachineUpgrades CurrentUpgrade(int addIndex = 0)
        {
            return machine.machineUpgrade.upgrades[machine.machineUpgrade.machineLevel];
        }

        private void UpdateTexts()
        {
            nameText.text = machine.name;

            //Upgrade

            upgradeInfoText.text = "Current level: " + (machine.machineUpgrade.machineLevel + 1) + "\nUpgrade costs: " + CurrentUpgrade().ironUpgradeCosts + " Iron";

            string _infoText = "";

            // Needs
            _infoText += "Requires: ";

            if (CurrentUpgrade().energyConsumptionPerSec > 0f)
                _infoText += "\n- Energy";

            if (CurrentUpgrade().unitInput.Length != 0)
            {
                for (int i = 0; i < CurrentUpgrade().unitInput.Length; i++)
                {
                    _infoText += ("\n- " + CurrentUpgrade().unitInput[i].amount + " " + CurrentUpgrade().unitInput[i].unit);
                }
            }
            else
            {
                if (CurrentUpgrade().energyConsumptionPerSec == 0f)
                    _infoText += "\n- Nothing";
            }

            // Produces
            if (CurrentUpgrade().unitOutput.Length != 0)
            {
                _infoText += "\nProduces: ";

                for (int i = 0; i < CurrentUpgrade().unitOutput.Length; i++)
                {
                    _infoText += ("\n- " + CurrentUpgrade().unitOutput[i].amount + " " + CurrentUpgrade().unitOutput[i].unit);
                }
            }

            infoText.text = machine.infoText + "\n\n" + _infoText;
        }
    }
}