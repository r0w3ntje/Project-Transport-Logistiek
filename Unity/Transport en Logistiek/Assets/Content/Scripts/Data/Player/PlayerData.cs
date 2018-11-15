using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using Systems.Singleton;
using UnityEngine.UI;

public class PlayerData : Singleton<PlayerData>
{
    public float euro;
    public int goods;

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        euro = PointSystem.Data(Action.Load, "euro", euro);
        goods = PointSystem.Data(Action.Load, "goods", goods);
    }

    private void SaveData()
    {
        euro = PointSystem.Data(Action.Save, "euro", euro);
        goods = PointSystem.Data(Action.Save, "goods", goods);
    }
}