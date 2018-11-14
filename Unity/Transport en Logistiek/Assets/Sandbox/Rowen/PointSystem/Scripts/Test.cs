using UnityEngine;

namespace Systems.PointSystem
{
    public class Test : MonoBehaviour
    {
        public int coins;
        public float fuel;

        private void Start()
        {
            coins = PointSystem<int>.Data(Action.Load, "coins", coins);
            fuel = PointSystem<float>.Data(Action.Load, "fuel", fuel);
        }

        [ContextMenu("Test")]
        private void Add()
        {
            coins = PointSystem<int>.Add(coins, 1);
            fuel = PointSystem<float>.Add(fuel, 0.422f);

            PointSystem<int>.Data(Action.Save, "coins", coins);
            PointSystem<float>.Data(Action.Save, "fuel", fuel);
        }
    }
}