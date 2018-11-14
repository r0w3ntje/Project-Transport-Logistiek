using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.PointSystem
{
    public class Test : MonoBehaviour
    {
        public int coins;
        public float fuel;

        private void Start()
        {
            coins = PointSystem<int>.Data(coins, Action.Load, "coins");
            fuel = PointSystem<float>.Data(fuel, Action.Load, "fuel");
        }

        [ContextMenu("Test")]
        private void Add()
        {
            coins = PointSystem<int>.Add(coins, 1);
            fuel = PointSystem<float>.Add(fuel, 0.421f);

            PointSystem<int>.Data(coins, Action.Save, "coins");
            PointSystem<float>.Data(fuel, Action.Save, "fuel");
        }
    }
}