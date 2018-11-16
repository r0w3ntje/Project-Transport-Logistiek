﻿using System.Collections;
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
        public Action currentTask;

        [SerializeField] private List<SuppliesPoint> suppliesPoints;
        [SerializeField] private SuppliesPoint currentSuppliesPoint;

        private void Awake()
        {
            DeliverEvent.OnDeliver += NewSuppliesPoint;
        }

        private void Start()
        {
            suppliesPoints = FindObjectsOfType<SuppliesPoint>().ToList();

            NewSuppliesPoint();
        }

        private void NewAction()
        {
            if (currentTask == Action.Pickup)
            {
                currentTask = Action.Deliver;
            }
            else if (currentTask == Action.Deliver)
            {
                currentTask = Action.Pickup;
            }
        }

        private void NewSuppliesPoint()
        {
            foreach (var suppliesPoint in suppliesPoints)
            {
                suppliesPoint.isActive = false;
            }

            currentSuppliesPoint = suppliesPoints[Random.Range(0, suppliesPoints.Capacity)];
            currentSuppliesPoint.isActive = true;
        }
    }
}