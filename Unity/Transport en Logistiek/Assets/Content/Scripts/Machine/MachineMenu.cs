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

        [Header("Is On")]
        [SerializeField] private Toggle isOnToggle;

        [Header("Produce")]
        [SerializeField] private Text produceText;
        [SerializeField] private Image produceButtonImage;

        [Header("Upgrade")]
        [SerializeField] private Text upgradeText;
        [SerializeField] private Image upgradeButtonImage;

        [Header("Colors")]
        [SerializeField] private Color active;
        [SerializeField] private Color inActive;

        [HideInInspector] public Machine machine;

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

        public void SetData(Machine _machine)
        {
            machine = _machine;

            //UpdateTexts();
        }

        //private void FixedUpdate()
        //{
        //    if (machine == null) return;

        //    //CheckProduction();
        //    CheckUpgrade();
        //}

        //private void CheckProduction()
        //{
        //    // Not producing at the moment
        //    if (machine.producing == null)
        //    {
        //        // Has enough resources for the production
        //        if (PlayerData.Instance().HasSufficientUnits(machine.unitInput, machine.upgrades[machine.machineLevel].neededAmount))
        //        {
        //            produceButtonImage.color = active;
        //            return;
        //        }

        //        produceButtonImage.color = inActive;
        //    }
        //    else produceButtonImage.color = inActive;
        //}

        //private void CheckUpgrade()
        //{
        //    // Has enough resources for the production
        //    if (PlayerData.Instance().HasSufficientUnits(UnitEnum.Ijzer, machine.upgrades[machine.machineLevel].ironUpgradeCosts))
        //    {
        //        upgradeButtonImage.color = active;
        //        return;
        //    }

        //    upgradeButtonImage.color = inActive;
        //}


        //public void Produce()
        //{
        //    machine.StartProduction();
        //}

        public void MachineOnOff()
        {
            machine.machineProduction.isOn = isOnToggle.isOn;
        }

        public void Upgrade()
        {
            machine.machineUpgrade.Upgrade();
        }

        private void UpdateTexts()
        {
            //nameText.text = machine.machineType.ToString();

            var upgrade = machine.machineUpgrade.upgrades[machine.machineUpgrade.machineLevel];

            //Produce
            produceText.text = "Produce: " + upgrade.unitOutputAmount + " '" + machine.machineProduction.unitOutput.ToString() + "'";

            if (machine.machineProduction.unitInput != UnitEnum.Geen)
            {
                produceText.text += "\nRequires: " + upgrade.unitInputAmount + " '" + machine.machineProduction.unitInput.ToString() + "'";
            }

            //Upgrade
            upgradeText.text = upgrade.unitOutputAmount + " '" + machine.machineProduction.unitOutput.ToString() + "' per production \nRequires " + upgrade.ironUpgradeCosts + " 'Iron'";
        }
    }
}