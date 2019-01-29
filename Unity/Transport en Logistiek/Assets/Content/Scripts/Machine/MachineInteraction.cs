using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TransportLogistiek
{
    public class MachineInteraction : MonoBehaviour
    {
        [Header("Audio")]
        [FMODUnity.EventRef]
        public string iron = "event:/Machines/IronRefinery_Producing";

        [FMODUnity.EventRef]
        public string power = "event:/Machines/Power";

        [FMODUnity.EventRef]
        public string oreMiner = "event:/Machines/Miner";

        FMOD.Studio.EventInstance producingIron;
        FMOD.Studio.EventInstance producingOre;
        FMOD.Studio.EventInstance producingPower;

        private Machine machine;

        private void Awake()
        {
            producingIron.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            producingOre.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            producingPower.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        }

        private void Start()
        {
            producingIron = FMODUnity.RuntimeManager.CreateInstance(iron);           
            producingPower = FMODUnity.RuntimeManager.CreateInstance(power);
            producingOre = FMODUnity.RuntimeManager.CreateInstance(oreMiner);

            machine = GetComponent<Machine>();
        }

        private void OnMouseEnter()
        {
            CursorHoverInfo.Instance().hoverText.text = machine.name;
            CursorHoverInfo.Instance().hoverText.enabled = true;
        }

        private void OnMouseExit()
        {
            CursorHoverInfo.Instance().hoverText.enabled = false;
        }

        private void OnMouseDown()
        {
            MachineMenu.Instance().Open(machine);

            if (this.gameObject.tag == "Iron_Refinery")
            {               
                Debug.Log("Sound");
                producingIron.start();
            }
            else if (this.gameObject.tag == "Power_Generator")
            {               
                Debug.Log("Sound");
                producingPower.start();
            }
            else if (this.gameObject.tag == "Miner")
            {
                Debug.Log("Sound");
                producingOre.start();
            }

            //producingPower.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            // producingIron.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
