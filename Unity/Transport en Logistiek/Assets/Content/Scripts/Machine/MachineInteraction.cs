using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TransportLogistiek
{
    public class MachineInteraction : MonoBehaviour
    {
        private Machine machine;

        private void Start()
        {
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
        }
    }
}
