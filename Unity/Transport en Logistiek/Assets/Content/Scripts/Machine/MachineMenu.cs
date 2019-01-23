﻿using System.Collections;
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

            isOnToggle.isOn = machine.machineProduction.isOn;
            UpdateTexts();
        }

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
            nameText.text = machine.machineType.ToString();

            var upgrade = machine.machineUpgrade.upgrades[machine.machineUpgrade.machineLevel];

            //Upgrade
            upgradeInfoText.text = upgrade.unitOutputAmount + " " + machine.machineProduction.unitOutput.ToString() + " per production \nRequires " + upgrade.ironUpgradeCosts + " Iron";

            infoText.text = machine.infoText;
        }
    }
}