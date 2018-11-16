using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using Systems.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : Singleton<PlayerData>
{
    public float money;
    public int goods;

    [Header("UI Elements")]

    [SerializeField] private Text moneyText;
    [SerializeField] private Text goodsText;

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        PointSystem.Data(Action.Load, "money", ref money);
        PointSystem.Data(Action.Load, "goods", ref goods);

        UpdateTexts();
    }

    [ContextMenu("Save")]
    private void SaveData()
    {
        PointSystem.Data(Action.Save, "money", ref money);
        PointSystem.Data(Action.Save, "goods", ref goods);

        UpdateTexts();
    }

    public void Add<T>(ref T _var, T _amount)
    {
        PointSystem.Add(ref _var, _amount);

        UpdateTexts();
    }

    private void UpdateTexts()
    {
        moneyText.text = "$" + money.ToString("F2");
        goodsText.text = goods.ToString();
    }
}