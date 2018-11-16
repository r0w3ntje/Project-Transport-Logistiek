using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Systems.Singleton;
using UnityEngine;

namespace Systems.QuestSystem
{
    using PointSystem;

    public enum Action
    {
        Pickup, Deliver
    }

    public class QuestManager : Singleton<QuestManager>
    {
        [Header("Keep the current task on DELIVER! (it toggles at start)")]
        public Action currentTask = Action.Deliver;

        [Space(8)]

        [SerializeField] private List<SuppliesPoint> suppliesPoints;
        [SerializeField] private SuppliesPoint currentSuppliesPoint;

        [Space(8)]
        [SerializeField] private GameObject missionPassed;
        [SerializeField] private GameObject deliver;

        [SerializeField] private float popupTime;

        private void Awake()
        {
            DeliverEvent.OnDeliver += NewSuppliesPoint;
        }

        private void Start()
        {
            suppliesPoints = FindObjectsOfType<SuppliesPoint>().ToList();

            NewSuppliesPoint();

            StopAllCoroutines();

            missionPassed.SetActive(false);
            deliver.SetActive(false);
        }

        private void NewAction()
        {
            if (currentTask == Action.Pickup)
            {
                StartCoroutine(MissionPassed(deliver));
                currentTask = Action.Deliver;
            }
            else if (currentTask == Action.Deliver)
            {
                StartCoroutine(MissionPassed(missionPassed));
                currentTask = Action.Pickup;
            }
        }

        [ContextMenu("New Supplies Point")]
        private void NewSuppliesPoint()
        {
            NewAction();

            foreach (var suppliesPoint in suppliesPoints)
            {
                suppliesPoint.isActive = false;
            }

            SuppliesPoint newSuppliesPoint;

            do
            {
                newSuppliesPoint = suppliesPoints[Random.Range(0, suppliesPoints.Capacity)];
            }
            while (newSuppliesPoint == currentSuppliesPoint);

            currentSuppliesPoint = newSuppliesPoint;

            currentSuppliesPoint.isActive = true;
        }

        private IEnumerator MissionPassed(GameObject _go)
        {
            _go.SetActive(true);
            yield return new WaitForSeconds(popupTime);
            _go.SetActive(false);
        }
    }
}