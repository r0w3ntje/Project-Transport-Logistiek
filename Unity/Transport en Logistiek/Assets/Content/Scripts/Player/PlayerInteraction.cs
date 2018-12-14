using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Systems.Singleton;
using UnityEngine;

namespace TransportLogistiek
{
    public class PlayerInteraction : Singleton<PlayerInteraction>
    {
        public KeyCode interactionKeyBind;
        [SerializeField] private KeyCode dropKeybind;
        public KeyCode upgradeKeyBind;

        [Space(8)]

        public bool isHolding;

        [Space(8)]

        public float interactDistance = 15f;

        [Space(8)]

        public Unit unit;
        public Machine machine;

        private void Start()
        {
            isHolding = false;
        }

        private void Update()
        {
            if (unit == null)
                isHolding = false;

            if (Input.GetKeyDown(dropKeybind) && unit != null)
            {
                DropUnit();
            }

            UseMachine();
            PickupUnit();
        }

        private void UseMachine()
        {
            List<Machine> machines = FindObjectsOfType<Machine>().ToList();

            if (machines == null) return;

            //float shortestDistance = Vector3.Distance(transform.position, machines[0].interactionObject.transform.position);

            machine = null;

            for (int i = 0; i < machines.Count; i++)
            {
                //float distance = Vector3.Distance(transform.position, machines[i].interactionObject.transform.position);

                //if (distance <= shortestDistance)
                //{
                //    shortestDistance = distance;
                //    machine = machines[i];
                //}
            }

            if (machine == null) return;

            if (isHolding || (isHolding || machine.neededUnit == UnitEnum.Geen) || (isHolding && machine.neededUnit == UnitEnum.Geen))
            {
                //if ((Input.GetKeyDown(interactionKeyBind) && shortestDistance <= interactDistance))
                //{
                //    if (machine.producing == null && (machine.neededUnit == UnitEnum.Geen || (unit != null && machine.neededUnit == unit.UnitType)))
                //    {
                //        machine.Produce();

                //        if (unit != null && machine.neededUnit != UnitEnum.Geen)
                //        {
                //            DestroyUnit();
                //        }
                //    }
                //}
            }

            //if ((Input.GetKeyDown(upgradeKeyBind) && shortestDistance <= interactDistance))
            //{
            //    if (machine.producing == null && unit != null)
            //    {
            //        if (unit.UnitType == UnitEnum.Ijzer)
            //        {
            //            machine.machineUpgrade.Upgrade();
            //            DestroyUnit();
            //        }
            //    }
            //}
        }

        private void PickupUnit()
        {
            if (Input.GetKeyDown(interactionKeyBind) && !isHolding)
            {
                GetUnit();

                if (unit != null)
                {
                    unit.GetComponent<Rigidbody>().useGravity = false;
                    unit.GetComponent<Rigidbody>().freezeRotation = true;
                    unit.transform.rotation = Quaternion.Euler(Vector3.zero);
                    //unit.follow = true;
                    isHolding = true;
                }
            }
        }

        private void GetUnit()
        {
            List<Unit> units = FindObjectsOfType<Unit>().ToList();

            if (units.Count == 0) return;

            float shortestDistance = Vector3.Distance(transform.position, units[0].transform.position);
            unit = null;

            for (int i = 0; i < units.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, units[i].transform.position);

                if (distance <= shortestDistance && distance <= interactDistance)
                {
                    shortestDistance = distance;
                    unit = units[i];
                }
            }
        }

        private void DropUnit()
        {
            if (!isHolding) return;

            unit.GetComponent<Rigidbody>().useGravity = true;
            unit.GetComponent<Rigidbody>().freezeRotation = false;
            unit.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //unit.follow = false;
            unit = null;

            //if (machine != null)
            //    machine.interactionText.enabled = false;
            machine = null;

            isHolding = false;
        }

        public void DestroyUnit()
        {
            isHolding = false;

            Destroy(unit.gameObject);

            unit = null;

            machine = null;
        }
    }
}