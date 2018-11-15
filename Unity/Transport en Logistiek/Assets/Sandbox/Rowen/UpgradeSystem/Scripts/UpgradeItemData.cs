using System.Collections.Generic;
using UnityEngine;

namespace Systems.UpgradeSystem
{
    [CreateAssetMenu(fileName = "UpgradeItem Data", menuName = "UpgradeSystem/UpgradeItem Data")]
    public class UpgradeItemData : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private List<Data> upgradeData;
    }

    [System.Serializable]
    public class Data
    {
        public float price;
    }
}