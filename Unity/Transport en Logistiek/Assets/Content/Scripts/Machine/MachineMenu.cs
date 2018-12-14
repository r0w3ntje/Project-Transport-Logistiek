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

        //[Header("Upgrade")]

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

        private void SetData(Machine _machine)
        {
            machine = _machine;
            nameText.text = _machine.machineType.ToString();

            produceText.text = "Produce: " + _machine.produceAmount + " '" + _machine.producedUnit.ToString() + "'";

            if (_machine.neededUnit != UnitEnum.Geen)
            {
                produceText.text += "\nRequires: " + _machine.neededUnitAmount + " '" + _machine.neededUnit.ToString() + "'";
            }
        }
    }
}