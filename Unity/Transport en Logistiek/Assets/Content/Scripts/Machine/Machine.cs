using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        pi = FindObjectOfType<PlayerInteraction>();
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
            //if (pi.unit == null && neededUnit != UnitEnum.Geen)
            //{
            //    interactionText.enabled = false;
            //}
            //if (neededUnit == UnitEnum.Geen)
            //{
            //    interactionText.enabled = true;
            //}
            //else if (pi.unit != null && pi.unit.UnitType == neededUnit)
            //{
            //    interactionText.enabled = true;
            //}
            //else interactionText.enabled = false;
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
        Debug.Log("Producing: " + producedUnit);
        interactionText.text = producedUnit + " is aan het produceren...";
        yield return new WaitForSeconds(producingTime);
        SpawnUnit();
        producing = null;
    }

    private void SpawnUnit()
    {
        var a = Instantiate(unitPrefab, unitSpawnPoint);
        a.transform.localPosition = Vector3.zero;
        a.transform.SetParent(null);
        a.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        a.GetComponent<Unit>().UnitType = producedUnit;
        SetText();
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
        }
    }
}
