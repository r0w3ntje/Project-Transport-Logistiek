﻿using System.Collections;
using System.Collections.Generic;
using Systems.Singleton;
using UnityEngine;

public class PlayerInteraction : Singleton<PlayerInteraction>
{
    public KeyCode interactionKeyBind;
    [SerializeField] private KeyCode dropKeybind;

    [Space(8)]

    public bool isHolding;

    [Space(8)]

    public GameObject unit;


    [SerializeField] private Transform unitParent;

    private void Update()
    {
        if (Input.GetKeyDown(dropKeybind))
        {
            DropUnit();
        }
    }

    private void Interact(GameObject _obj)
    {
        if (Input.GetKeyDown(interactionKeyBind))
        {
            switch (_obj.tag)
            {
                case "Unit":
                    PickupUnit(_obj);
                    break;
            }
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

    private void OnTriggerStay(Collider other)
    {
        Interact(other.gameObject);
    }
}