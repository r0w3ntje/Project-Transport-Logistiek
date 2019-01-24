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

            if (machine.machineProduction.machineState == MachineStateEnum.On)
                machine.machineProduction.StartProduction();
        }

        public void Upgrade()
        {
            machine.machineUpgrade.Upgrade();
        }

        private void UpdateTexts()
        {
            nameText.text = machine.name;

            var upgrade = machine.machineUpgrade.upgrades[machine.machineUpgrade.machineLevel];

            //Upgrade
            //upgradeInfoText.text = upgrade.unitOutputAmount + " " + machine.machineProduction.unitOutput.ToString() + " per production \nRequires " + upgrade.ironUpgradeCosts + " Iron";

            infoText.text = machine.infoText;
        }
    }
}