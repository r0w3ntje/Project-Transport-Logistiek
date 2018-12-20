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

        [Header("Produce")]
        [SerializeField] private Text produceText;

        [Header("Upgrade")]
        [SerializeField] private Text upgradeText;

        private Machine machine;

        private void Start()
        {
            Close();
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

        public void Produce()
        {
            machine.Produce();
        }

        public void Upgrade()
        {
            machine.Upgrade();
        }

        private void SetData(Machine _machine)
        {
            machine = _machine;

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            nameText.text = machine.machineType.ToString();

            //Produce
            produceText.text = "Produce: " + machine.upgrades[machine.machineLevel].producingAmount + " '" + machine.producedUnit.ToString() + "'";

            if (machine.neededUnit != UnitEnum.Geen)
            {
                produceText.text += "\nRequires: " + machine.upgrades[machine.machineLevel].neededAmount + " '" + machine.neededUnit.ToString() + "'";
            }

            //Upgrade
            upgradeText.text = machine.upgrades[machine.machineLevel].producingAmount + " '" + machine.producedUnit.ToString() + "' per production \nRequires " + machine.upgrades[machine.machineLevel].ironUpgradeCosts + " 'Iron'";
        }
    }
}