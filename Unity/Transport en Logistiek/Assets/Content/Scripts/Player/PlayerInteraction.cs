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

    public float interactDistance = 3f;

    [Space(8)]

    public Unit unit;
    public Machine machine;

    private void Start()
    {
        isHolding = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(dropKeybind) && unit != null)
        {
            DropUnit();
        }

        if (isHolding) UseMachine();
        else PickupUnit();
    }

    private void UseMachine()
    {
        List<Machine> machines = FindObjectsOfType<Machine>().ToList();

        if (machines == null) return;
        if (unit == null) return;

        float shortestDistance = Vector3.Distance(transform.position, machines[0].interactionObject.transform.position);

        machine = null;

        for (int i = 0; i < machines.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, machines[i].interactionObject.transform.position);

            if (distance <= shortestDistance)
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

                if (unit != null)
                {
                    DestroyUnit();
                }
            }
        }
    }

    private void PickupUnit()
    {
        if (Input.GetKeyDown(interactionKeyBind) && !isHolding)
        {
            if (machine != null)
            {
                if (machine.producing != null)
                {
                    return;
                }
            }

            GetUnit();

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

    private void GetUnit()
    {
        List<Unit> units = FindObjectsOfType<Unit>().ToList();

        Debug.Log("List " + units + ", " + units.Count);

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

        machine = null;
    }
}