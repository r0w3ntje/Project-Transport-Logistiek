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
            PointSystem.Data(Action.Load, "coins", ref coins);
            PointSystem.Data(Action.Load, "fuel", ref fuel);
            PointSystem.Data(Action.Load, "playerName", ref playerName);
        }

        [ContextMenu("Test")]
        private void Add()
        {
            PointSystem.Add(ref coins, 1);
            PointSystem.Add(ref fuel, 0.422f);

            PointSystem.Data(Action.Save, "coins", ref coins);
            PointSystem.Data(Action.Save, "fuel", ref fuel);
            PointSystem.Data(Action.Save, "playerName", ref playerName);
        }

        [ContextMenu("Reset")]
        private void Delete()
        {
            PointSystem.Data(Action.Reset, "coins", ref coins);
            PointSystem.Data(Action.Reset, "fuel", ref fuel);
            PointSystem.Data(Action.Reset, "playerName", ref playerName);
        }
    }
}