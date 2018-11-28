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
    //public Text interactionText;

    //private void Start()
    //{
    //interactionText.enabled = false;
    //}

    public void Produce()
    {
        if (producedUnit != UnitEnum.None)
            StartCoroutine(Producing());
    }

    public IEnumerator Producing()
    {
        Debug.Log("Producing: " + producedUnit);
        yield return new WaitForSeconds(producingTime);
        SpawnUnit();
    }

    private void SpawnUnit()
    {
        var a = Instantiate(unitPrefab, unitSpawnPoint);
        a.transform.localPosition = Vector3.zero;
        a.transform.SetParent(null);
        a.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        a.GetComponent<Unit>().UnitType = producedUnit;
    }
}
