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

    private Unit unit;
    private Machine machine;

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

        Interact();
    }

    private void Interact()
    {
        if (isHolding) UseMachine();
        else PickupUnit();
    }

    private void UseMachine()
    {
        List<Machine> machines = FindObjectsOfType<Machine>().ToList();

        float shortestDistance = Vector3.Distance(transform.position, machines[0].interactionObject.transform.position);
        machine = machines[0];

        for (int i = 0; i < machines.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, machines[i].interactionObject.transform.position);

            if (distance < shortestDistance && distance <= interactDistance)
            {
                shortestDistance = distance;
                machine = machines[i];
            }
        }

        if (Input.GetKeyDown(interactionKeyBind) && shortestDistance <= interactDistance)
        {
            if (machine.neededUnit == UnitEnum.None || (unit != null && machine.neededUnit == unit.UnitType))
            {
                machine.Produce();
                DestroyUnit();
            }
        }
    }

    private void PickupUnit()
    {
        if (Input.GetKeyDown(interactionKeyBind))
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
        unit = units[0];

        for (int i = 0; i < units.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, units[i].transform.position);

            if (distance < shortestDistance && distance <= interactDistance)
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
        isHolding = false;

        unit = null;
        machine = null;
    }

    public void DestroyUnit()
    {
        Destroy(unit.gameObject);
        unit = null;

        isHolding = false;
    }
}