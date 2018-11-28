using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Systems.Singleton;
using UnityEngine;

public class PlayerInteraction : Singleton<PlayerInteraction>
{
    public KeyCode interactionKeyBind;
    [SerializeField] private KeyCode dropKeybind;

    [Space(8)]

    public bool isHolding;

    [Space(8)]

    [SerializeField] private float interactDistance = 3f;

    [Space(8)]

    public Unit unit;
    public Machine machine;

    private void Start()
    {
        isHolding = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(dropKeybind))
        {
            DropUnit();
        }

        if (isHolding || (machine != null && machine.neededUnit == UnitEnum.None) || machine == null)
            UseMachine();
        //else

        if (!isHolding)
            PickupUnit();

        // Indicator text
        if (machine != null)
        {
            if (Vector3.Distance(transform.position, machine.interactionObject.transform.position) <= interactDistance)
            {
                if (unit != null)
                    machine.interactionText.enabled = isHolding && unit.UnitType == machine.neededUnit;
                else
                    machine.interactionText.enabled = !isHolding;
            }
            else machine.interactionText.enabled = false;
        }
    }

    private void UseMachine()
    {
        List<Machine> machines = FindObjectsOfType<Machine>().ToList();

        float shortestDistance = Vector3.Distance(transform.position, machines[0].interactionObject.transform.position);
        if (machine != null) machine.interactionText.enabled = false;
        machine = null;

        for (int i = 0; i < machines.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, machines[i].interactionObject.transform.position);

            if (distance <= shortestDistance && distance <= interactDistance)
            {
                shortestDistance = distance;
                machine = machines[i];
            }
        }

        if (machine == null) return;

        if ((Input.GetKeyDown(interactionKeyBind) && shortestDistance <= interactDistance) || (Input.GetKeyDown(interactionKeyBind) && machine.neededUnit == UnitEnum.None))
        {
            if (machine.neededUnit == UnitEnum.None || (unit != null && machine.neededUnit == unit.UnitType))
            {
                machine.Produce();

                if (machine.neededUnit != UnitEnum.None)
                    DestroyUnit();
            }
        }
    }

    private void PickupUnit()
    {
        if (Input.GetKeyDown(interactionKeyBind) && !isHolding)
        {
            unit = GetUnit();

            if (unit != null)
            {
                unit.GetComponent<Rigidbody>().useGravity = false;
                unit.GetComponent<Rigidbody>().freezeRotation = true;
                unit.transform.rotation = Quaternion.Euler(Vector3.zero);
                unit.follow = true;
                isHolding = true;
            }
        }
    }

    private Unit GetUnit()
    {
        List<Unit> units = FindObjectsOfType<Unit>().ToList();

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

        return unit;
    }

    private void DropUnit()
    {
        if (!isHolding) return;

        unit.GetComponent<Rigidbody>().useGravity = true;
        unit.GetComponent<Rigidbody>().freezeRotation = false;
        unit.GetComponent<Rigidbody>().velocity = Vector3.zero;
        unit.follow = false;
        unit = null;

        if (machine != null)
            machine.interactionText.enabled = false;
        machine = null;

        isHolding = false;
    }

    public void DestroyUnit()
    {
        isHolding = false;

        Destroy(unit.gameObject);
        unit = null;

        machine.interactionText.enabled = false;
        machine = null;

    }
}