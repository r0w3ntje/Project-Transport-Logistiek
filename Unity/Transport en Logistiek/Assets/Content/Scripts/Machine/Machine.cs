using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using UnityEngine;
using UnityEngine.UI;

namespace TransportLogistiek
{
    [RequireComponent(typeof(MachineUpgrade))]
    [RequireComponent(typeof(MachineProduction))]
    [RequireComponent(typeof(MachineInteraction))]
    public class Machine : MonoBehaviour
    {
        #region Fields

        [Header("ID")]
        public string uniqueID;

        [Header("Machine")]
        public MachineEnum machineType;

        [HideInInspector] public MachineProduction machineProduction;
        [HideInInspector] public MachineUpgrade machineUpgrade;

        public string infoText;

        #endregion

        private void Start()
        {
            machineProduction = GetComponent(typeof(MachineProduction)) as MachineProduction;
            machineUpgrade = GetComponent(typeof(MachineUpgrade)) as MachineUpgrade;
        }

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