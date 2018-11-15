using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using Systems.Singleton;

public class PlayerData : Singleton<PlayerData>
{
    public float euro;
    public int trash;

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        euro = PointSystem.Data(Action.Load, "euro", euro);
        trash = PointSystem.Data(Action.Load, "trash", trash);

        SaveData();
    }

    private void SaveData()
    {
        euro = PointSystem.Data(Action.Save, "euro", euro);
        trash = PointSystem.Data(Action.Save, "trash", trash);
    }
}