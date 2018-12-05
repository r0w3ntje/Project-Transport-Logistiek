using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MachineUpgrade))]
public class Machine : MonoBehaviour
{
    [Header("Some need an unit to start with (can be none)")]
    public UnitEnum neededUnit;
    public UnitEnum producedUnit;

    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform unitSpawnPoint;

    public Transform interactionObject;
    public Text interactionText;

    public Coroutine producing;

    private PlayerInteraction pi;

    [HideInInspector] public MachineUpgrade machineUpgrade;

    private void Start()
    {
        pi = FindObjectOfType<PlayerInteraction>();
        machineUpgrade = GetComponent<MachineUpgrade>();

        SetText();
    }

    private void FixedUpdate()
    {
        ShowText();
    }

    private void ShowText()
    {
        if (Vector3.Distance(pi.transform.position, interactionObject.position) <= pi.interactDistance)
        {
            interactionText.enabled = true;
            machineUpgrade.upgradeText.enabled = true;
        }
        else
        {
            interactionText.enabled = false;
            machineUpgrade.upgradeText.enabled = false;
        }
    }

    public void Produce()
    {
        if (producedUnit != UnitEnum.Geen)
        {
            if (producing == null)
                producing = StartCoroutine(Producing());
        }
    }

    public IEnumerator Producing()
    {
        interactionText.text = producedUnit + " is aan het produceren...";

        yield return new WaitForSeconds(machineUpgrade.producingTime);

        AddUnits();
        SpawnUnit();
        SetText();

        producing = null;
    }

    private void SpawnUnit()
    {
        var a = Instantiate(unitPrefab, unitSpawnPoint);
        a.transform.localPosition = Vector3.zero;
        a.transform.SetParent(null);
        a.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        a.GetComponent<Unit>().UnitType = producedUnit;
    }

    private void AddUnits()
    {
        switch (producedUnit)
        {
            case UnitEnum.Ijzer:
                PlayerData.Instance().Add(ref PlayerData.Instance().iron, machineUpgrade.amountPerProducing);
                break;
            case UnitEnum.Voedsel:
                PlayerData.Instance().Add(ref PlayerData.Instance().food, machineUpgrade.amountPerProducing);
                break;
            case UnitEnum.Erts:
                PlayerData.Instance().Add(ref PlayerData.Instance().ore, machineUpgrade.amountPerProducing);
                break;
        }
    }

    public void SetText()
    {
        interactionText.text = "Gebruik '" + PlayerInteraction.Instance().interactionKeyBind + "'";

        switch (neededUnit)
        {
            case UnitEnum.Geen:
                interactionText.text = "";
                break;
            case UnitEnum.Ijzer:
            case UnitEnum.Voedsel:
            case UnitEnum.Erts:
                interactionText.text += "\nHeeft een " + neededUnit.ToString() + " krat nodig!\n";
                break;
        }

        interactionText.text += "\nProduceert " + machineUpgrade.amountPerProducing + " " + producedUnit + " in " + machineUpgrade.producingTime + " seconden";
    }
}
