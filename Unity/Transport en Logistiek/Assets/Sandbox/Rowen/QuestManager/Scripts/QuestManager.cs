using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.QuestSystem
{
    using PointSystem;

    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private List<DeliverPoint> deliverPoints;

        private void Awake()
        {
            CollectEvent.OnCollect += ItemCollected;
        }

        private void Start()
        {

        }

        private void ItemCollected()
        {

        }
    }
}