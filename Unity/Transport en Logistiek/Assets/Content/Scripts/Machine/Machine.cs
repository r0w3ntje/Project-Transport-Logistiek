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
        [Header("Production")]
        public UnitEnum neededUnit;
        public UnitEnum producedUnit;

        [Header("Unit")]
        [SerializeField] private GameObject unitPrefab;

        [Header("Interaction")]
        [SerializeField] private Transform unitSpawnPoint;
        public Transform interactionObject;

        [Header("Texts")]
        public Text interactionText;

        [Header("Audio")]
        [FMODUnity.EventRef] public string iron_Producing = "event:/Machines/IronRefinery_Producing";
        [FMODUnity.EventRef] public string miner_Producing = "event:/Machines/Miner";

        FMOD.Studio.EventInstance Iron_Producing;
        FMOD.Studio.EventInstance Miner;

        // Other
        [HideInInspector] public MachineUpgrade machineUpgrade;
        public Coroutine producing;

        private void Start()
        {
            machineUpgrade = GetComponent<MachineUpgrade>();

            SetText();

            if (gameObject.tag == "Miner") PlayAudio(Miner, miner_Producing);
        }

        private void FixedUpdate()
        {
            ShowText();
        }

        private void ShowText()
        {
            if (Vector3.Distance(PlayerInteraction.Instance().transform.position, interactionObject.position) <= PlayerInteraction.Instance().interactDistance)
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

            if (gameObject.tag == "Iron_Refinery") PlayAudio(Iron_Producing, iron_Producing);

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

        private void PlayAudio(FMOD.Studio.EventInstance _fmodEventInstance, string _instance)
        {
            _fmodEventInstance = FMODUnity.RuntimeManager.CreateInstance(_instance);

            _fmodEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

            _fmodEventInstance.start();

            _fmodEventInstance.setParameterValue("IsProducing", 1f);
        }
    }
}