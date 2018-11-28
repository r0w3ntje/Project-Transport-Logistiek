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

    [SerializeField] private float unitPickupDistance = 1f;

    [Space(8)]

    public GameObject unit;


    [SerializeField] private Transform unitParent;

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
        if (Input.GetKeyDown(interactionKeyBind))
        {
            Debug.Log("Input");

            List<Unit> units = FindObjectsOfType<Unit>().ToList();

            Debug.Log(units.Count);

            if (units == null) return;

            GameObject unit = null;

            for (int i = 0; i < units.Count; i++)
            {
                if (Vector3.Distance(transform.position, units[i].transform.position) <= unitPickupDistance)
                {
                    unit = units[i].gameObject;
                }
            }

            if (unit != null)
                PickupUnit(unit);
        }
    }

    private void PickupUnit(GameObject _obj)
    {
        if (isHolding) return;

        unit = _obj;
        unit.GetComponent<Rigidbody>().useGravity = false;
        unit.GetComponent<Rigidbody>().velocity = Vector3.zero;
        unit.transform.SetParent(unitParent);
        unit.transform.localPosition = Vector3.zero;

        isHolding = true;
    }

    private void DropUnit()
    {
        if (!isHolding) return;

        unit.GetComponent<Rigidbody>().useGravity = true;
        unit.transform.SetParent(null);
        unit = null;

        isHolding = false;
    }

    public void DestroyUnit()
    {
        isHolding = false;

        Destroy(unit);

        unit = null;

        Debug.Log("Destroy Unit");
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    Interact(other.gameObject);
    //}
}