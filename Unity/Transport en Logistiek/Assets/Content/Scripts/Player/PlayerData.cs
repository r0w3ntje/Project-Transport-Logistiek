using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using Systems.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : Singleton<PlayerData>
{
    //public float money;
    //public int supplies;

    public int food;
    public int iron;
    public int ore;

    [Header("UI Elements")]

    //[SerializeField] private Text moneyText;
    //[SerializeField] private Text goodsText;

    [SerializeField] private Text foodText;
    [SerializeField] private Text ironText;
    [SerializeField] private Text oreText;

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        //PointSystem.Data(Action.Load, "money", ref money);
        //PointSystem.Data(Action.Load, "supplies", ref supplies);

        PointSystem.Data(Action.Load, "food", ref food);
        PointSystem.Data(Action.Load, "iron", ref iron);
        PointSystem.Data(Action.Load, "ore", ref ore);

        UpdateTexts();
    }

    [ContextMenu("Save")]
    private void SaveData()
    {
        //PointSystem.Data(Action.Save, "money", ref money);
        //PointSystem.Data(Action.Save, "supplies", ref supplies);

        PointSystem.Data(Action.Save, "food", ref food);
        PointSystem.Data(Action.Save, "iron", ref iron);
        PointSystem.Data(Action.Save, "iron", ref ore);

        UpdateTexts();
    }

    public void Add<T>(ref T _var, T _amount)
    {
        PointSystem.Add(ref _var, _amount);

        SaveData();

        UpdateTexts();
    }

    private void UpdateTexts()
    {
        //moneyText.text = "$" + money.ToString("F2");
        //goodsText.text = "Supplies: " + supplies;

        foodText.text = food.ToString();
        ironText.text = iron.ToString();
        oreText.text = ore.ToString();
    }
}