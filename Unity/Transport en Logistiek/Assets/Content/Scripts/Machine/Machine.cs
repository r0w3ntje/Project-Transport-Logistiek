using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    [Header("Some need an unit to start with (can be none)")]
    [SerializeField] private UnitEnum neededUnit;
    [SerializeField] private UnitEnum producedUnit;

    [SerializeField] private float producingTime;

    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private Transform unitSpawnPoint;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(other.GetComponent<PlayerInteraction>().interactionKeyBind))
            {
                var pi = other.GetComponent<PlayerInteraction>();

                if (neededUnit == UnitEnum.None || (pi.unit != null && neededUnit == pi.unit.GetComponent<Unit>().UnitType))
                {
                    pi.DestroyUnit();
                    StartCoroutine(Producing());
                }
            }
        }
    }

    public IEnumerator Producing()
    {
        Debug.Log("Producing");
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
