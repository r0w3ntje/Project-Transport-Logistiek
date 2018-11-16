using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Systems.QuestSystem
{
    using PointSystem;

    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private List<DeliverPoint> deliverPoints;
        [SerializeField] private DeliverPoint currentDeliverPoint;

        private void Awake()
        {
            DeliverEvent.OnDeliver += NewDeliverPoint;
        }

        private void Start()
        {
            deliverPoints = FindObjectsOfType<DeliverPoint>().ToList();

            NewDeliverPoint();
        }

        private void NewDeliverPoint()
        {
            foreach (var deliverPoint in deliverPoints)
            {
                deliverPoint.isActive = false;
            }

            currentDeliverPoint = deliverPoints[Random.Range(0, deliverPoints.Capacity)];
            currentDeliverPoint.isActive = true;
        }
    }
}