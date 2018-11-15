using UnityEngine;

namespace Systems.PointSystem
{
    public class PointSystemExample : MonoBehaviour
    {
        [SerializeField] private int coins;
        [SerializeField] private float fuel;
        [SerializeField] private string playerName;

        private void Start()
        {
            coins = PointSystem.Data(Action.Load, "coins", coins);
            fuel = PointSystem.Data(Action.Load, "fuel", fuel);
            playerName = PointSystem.Data(Action.Load, "playerName", playerName);
        }

        [ContextMenu("Test")]
        private void Add()
        {
            coins = PointSystem.Add(coins, 1);
            fuel = PointSystem.Add(fuel, 0.422f);

            PointSystem.Data(Action.Save, "coins", coins);
            PointSystem.Data(Action.Save, "fuel", fuel);
            PointSystem.Data(Action.Save, "playerName", playerName);
        }

        [ContextMenu("Reset")]
        private void Delete()
        {
            coins = PointSystem.Data(Action.Reset, "coins", coins);
            fuel = PointSystem.Data(Action.Reset, "fuel", fuel);
            playerName = PointSystem.Data(Action.Reset, "playerName", playerName);
        }
    }
}