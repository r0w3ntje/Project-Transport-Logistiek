using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using Systems.Singleton;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MachineUpgrade))]
public class Machine : MonoBehaviour
{
    [Header("Some need an unit to start with (can be none)")]
    public UnitEnum neededUnit;
    public UnitEnum producedUnit;

    [SerializeField] private float producingTime;

    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform unitSpawnPoint;

    public Transform interactionObject;
    public Text interactionText;

    public Coroutine producing;

    private PlayerInteraction pi;

    [SerializeField] private MachineUpgrade machineUpgrade;

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
        }
        else
        {
            interactionText.enabled = false;
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
        Debug.Log("Producing: " + producedUnit + ", in " + machineUpgrade.machineTimer + " seconds");

        interactionText.text = producedUnit + " is aan het produceren...";

        yield return new WaitForSeconds(machineUpgrade.machineTimer);

        AddUnits();
        SpawnUnit();
        machineUpgrade.GainExperience();
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
                PointSystem.Add(ref PlayerData.Instance().iron, 1);
                break;
            case UnitEnum.Voedsel:
                PointSystem.Add(ref PlayerData.Instance().food, 1);
                break;
            case UnitEnum.Erts:
                PointSystem.Add(ref PlayerData.Instance().ore, 1);
                break;
        }
    }

    private void SetText()
    {
        switch (neededUnit)
        {
            case UnitEnum.Geen:
                interactionText.text = "Gebruik 'E'";
                break;
            case UnitEnum.Ijzer:
                interactionText.text = "Gebruik 'E'\nHeeft een ijzer krat nodig!";
                break;
            case UnitEnum.Voedsel:
                interactionText.text = "Gebruik 'E'\nHeeft een voedsel krat nodig!";
                break;
            case UnitEnum.Erts:
                interactionText.text = "Gebruik 'E'\nHeeft een erts krat nodig!";
                break;
        }
    }
}
