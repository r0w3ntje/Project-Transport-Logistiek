using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using UnityEngine;
using UnityEngine.UI;

namespace TransportLogistiek
{
    public class Machine : MonoBehaviour
    {
        #region Fields
        [Header("ID")]
        [SerializeField] private string uniqueID;

        [Header("Machine")]
        public MachineEnum machineType;
        public int machineLevel;
        public bool machineIsOn;

        [Header("Production")]
        public UnitEnum neededUnit;
        public UnitEnum producedUnit;

        [Header("Prefabs")]
        [SerializeField] private GameObject unitPrefab;

        [Header("Upgrades")]
        public List<MachineUpgrades> upgrades;

        //[Header("Interaction")]
        //[SerializeField] private Transform unitSpawnPoint;
        //public Transform interactionObject;

        //[Header("Texts")]
        //public Text interactionText;

        ////[Header("Audio")]
        //[FMODUnity.EventRef] public string iron_Producing = "event:/Machines/IronRefinery_Producing";
        //[FMODUnity.EventRef] public string miner_Producing = "event:/Machines/Miner";

        //private FMOD.Studio.EventInstance Iron_Producing;
        //private FMOD.Studio.EventInstance Miner;

        // Other
        //[HideInInspector] public MachineUpgrade machineUpgrade;

        public Coroutine producing;

        #endregion

        #region Interaction

        private void OnMouseEnter()
        {
            CursorHoverInfo.Instance().hoverText.text = machineType.ToString();
            CursorHoverInfo.Instance().hoverText.enabled = true;
        }

        private void OnMouseExit()
        {
            CursorHoverInfo.Instance().hoverText.enabled = false;
        }

        private void OnMouseDown()
        {
            MachineMenu.Instance().Open(this);
        }

        #endregion

        private void Start()
        {
            LoadMachineLevel();

            //PointSystem.Data(Action.Load, uniqueID, ref machineLevel);

            //machineUpgrade = GetComponent<MachineUpgrade>();

            //SetText();

            //PlayAudio();
        }

        //private void FixedUpdate()
        //{
        //    ShowText();
        //}

        //private void ShowText()
        //{
        //    if (Vector3.Distance(PlayerInteraction.Instance().transform.position, interactionObject.position) <= PlayerInteraction.Instance().interactDistance)
        //    {
        //        interactionText.enabled = true;
        //        machineUpgrade.upgradeText.enabled = true;
        //    }
        //    else
        //    {
        //        interactionText.enabled = false;
        //        machineUpgrade.upgradeText.enabled = false;
        //    }
        //}

        #region Production

        public void Produce()
        {
            if (PlayerData.Instance().HasSufficientUnits(neededUnit, upgrades[machineLevel].neededAmount))
            {
                if (producedUnit != UnitEnum.Geen)
                {
                    if (producing == null)
                        producing = StartCoroutine(Producing());
                }
            }
        }

        public IEnumerator Producing()
        {
            AddUnits(neededUnit, -upgrades[machineLevel].neededAmount);

            PlayAudio();

            yield return new WaitForSeconds(upgrades[machineLevel].producingTime);

            //StartCoroutine(SpawnUnit());
            //SetText();

            AddUnits(producedUnit, upgrades[machineLevel].producingAmount);

            producing = null;
        }

        #endregion

        #region Upgrades

        public void Upgrade()
        {
            if (PlayerData.Instance().HasSufficientUnits(UnitEnum.Ijzer, upgrades[machineLevel].ironUpgradeCosts))
            {
                machineLevel++;
                SaveMachineLevel();
            }
        }

        private void SaveMachineLevel()
        {
            PlayerPrefs.SetInt(uniqueID, machineLevel);
        }

        private void LoadMachineLevel()
        {
            if (!PlayerPrefs.HasKey(uniqueID))
                SaveMachineLevel();
            machineLevel = PlayerPrefs.GetInt(uniqueID);

            machineLevel = 0;
        }

        [System.Serializable]
        public class MachineUpgrades
        {
            public int ironUpgradeCosts;

            public int producingAmount;
            public int neededAmount;

            public float energyConsumption;

            public float producingTime;
        }

        #endregion

        #region Maintenance

        [Header("Maintenance")]
        [SerializeField] private bool isKaput;
        [SerializeField] private float kaputTimer;
        [SerializeField] private int kaputSchadeInIjzer;

        #endregion

        //private IEnumerator SpawnUnit()
        //{
        //    for (int i = 0; i < machineUpgrade.amountPerProducing; i++)
        //    {
        //        //Iron_Producing.setParameterValue("IsProducing", 0f);

        //        var a = Instantiate(unitPrefab, unitSpawnPoint);
        //        a.transform.localPosition = Vector3.zero;
        //        a.transform.SetParent(null);
        //        a.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        //        a.GetComponent<Unit>().UnitType = producedUnit;

        //        AddUnits(producedUnit, 1);

        //        yield return new WaitForSeconds(0.5f);
        //    }
        //}

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

        //public void SetText()
        //{
        //    interactionText.text = "Gebruik '" + PlayerInteraction.Instance().interactionKeyBind + "'";

        //    switch (neededUnit)
        //    {
        //        case UnitEnum.Geen:
        //            interactionText.text = "";
        //            break;
        //        case UnitEnum.Ijzer:
        //        case UnitEnum.Voedsel:
        //        case UnitEnum.Erts:
        //            interactionText.text += "\nHeeft een " + neededUnit.ToString() + " krat nodig!\n";
        //            break;
        //    }

        //    interactionText.text += "\nProduceert " + machineUpgrade.amountPerProducing + " " + producedUnit + " in " + machineUpgrade.producingTime + " seconden";
        //}

        #region Fmod Audio

        private void PlayAudio()
        {
            //if (gameObject.tag == "Iron_Refinery") FMODAudio(Iron_Producing, iron_Producing);
            //if (gameObject.tag == "Miner") FMODAudio(Miner, miner_Producing);
        }

        private void FMODAudio(FMOD.Studio.EventInstance _fmodEventInstance, string _instance)
        {
            _fmodEventInstance = FMODUnity.RuntimeManager.CreateInstance(_instance);

            _fmodEventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

            _fmodEventInstance.start();

            _fmodEventInstance.setParameterValue("IsProducing", 1f);
        }

        #endregion
    }
}