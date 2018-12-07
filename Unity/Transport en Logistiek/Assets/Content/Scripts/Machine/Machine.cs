using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using UnityEngine;
using UnityEngine.UI;

namespace TransportLogistiek
{
    [RequireComponent(typeof(MachineUpgrade))]
    public class Machine : MonoBehaviour
    {
        [Header("Some need an unit to start with (can be none)")]
        public UnitEnum neededUnit;
        public UnitEnum producedUnit;

        [SerializeField] private GameObject unitPrefab;
        [SerializeField] private Transform unitSpawnPoint;

        public Transform interactionObject;
        public Text interactionText;

        public Coroutine producing;

        private PlayerInteraction pi;

        [Header("Audio")]
        [FMODUnity.EventRef]
        public string iron_Producing = "event:/Machines/IronRefinery_Producing";

        [FMODUnity.EventRef]
        public string miner_Producing = "event:/Machines/Miner";

        FMOD.Studio.EventInstance Iron_Producing;

        FMOD.Studio.EventInstance Miner;

        [HideInInspector] public MachineUpgrade machineUpgrade;

        private void Start()
        {
            pi = FindObjectOfType<PlayerInteraction>();
            machineUpgrade = GetComponent<MachineUpgrade>();

            SetText();
        }

        private void FixedUpdate()
        {
            ShowText();
        }

        private void ShowText()
        {
            if (Vector3.Distance(pi.transform.position, interactionObject.position) <= pi.interactDistance)
            {
                interactionText.enabled = true;
                machineUpgrade.upgradeText.enabled = true;
            }
            else
            {
                interactionText.enabled = false;
                machineUpgrade.upgradeText.enabled = false;
            }
        }

        public void Produce()
        {
            if (producedUnit != UnitEnum.Geen)
            {
                if (producing == null)
                    producing = StartCoroutine(Producing());
            }
        }

        public IEnumerator Producing()
        {
            interactionText.text = producedUnit + " is aan het produceren...";

            AddUnits(neededUnit, -1);
            if (gameObject.tag == "Miner")
            {
                Miner = FMODUnity.RuntimeManager.CreateInstance(miner_Producing);

                Miner.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

                Miner.start();

                Miner.setParameterValue("IsProducing", 1f);
            }

            if (gameObject.tag == "Iron_Refinery")
            {
                Iron_Producing = FMODUnity.RuntimeManager.CreateInstance(iron_Producing);

                Iron_Producing.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

                Iron_Producing.start();

                Iron_Producing.setParameterValue("IsProducing", 1f);

            }

            yield return new WaitForSeconds(machineUpgrade.producingTime);

            StartCoroutine(SpawnUnit());
            SetText();

            producing = null;
        }

        private IEnumerator SpawnUnit()
        {
            for (int i = 0; i < machineUpgrade.amountPerProducing; i++)
            {
                Iron_Producing.setParameterValue("IsProducing", 0f);

                var a = Instantiate(unitPrefab, unitSpawnPoint);
                a.transform.localPosition = Vector3.zero;
                a.transform.SetParent(null);
                a.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                a.GetComponent<Unit>().UnitType = producedUnit;

                AddUnits(producedUnit, 1);

                yield return new WaitForSeconds(0.5f);
            }
        }

        private void AddUnits(UnitEnum _unit, int _amount)
        {
            switch (_unit)
            {
                case UnitEnum.Ijzer:
                    PlayerData.Instance().Add(ref PlayerData.Instance().iron, _amount);
                    break;
                case UnitEnum.Voedsel:
                    PlayerData.Instance().Add(ref PlayerData.Instance().food, _amount);
                    break;
                case UnitEnum.Erts:
                    PlayerData.Instance().Add(ref PlayerData.Instance().ore, _amount);
                    break;
            }
        }

        public void SetText()
        {
            interactionText.text = "Gebruik '" + PlayerInteraction.Instance().interactionKeyBind + "'";

            switch (neededUnit)
            {
                case UnitEnum.Geen:
                    interactionText.text = "";
                    break;
                case UnitEnum.Ijzer:
                case UnitEnum.Voedsel:
                case UnitEnum.Erts:
                    interactionText.text += "\nHeeft een " + neededUnit.ToString() + " krat nodig!\n";
                    break;
            }

            interactionText.text += "\nProduceert " + machineUpgrade.amountPerProducing + " " + producedUnit + " in " + machineUpgrade.producingTime + " seconden";
        }
    }
}