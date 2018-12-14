using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using Systems.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : Singleton<PlayerData>
{
    [Header("Variables")]
    public int food;
    public int iron;
    public int ore;
    public float energy;

    [Header("UI Elements")]
    [SerializeField] private Text foodText;
    [SerializeField] private Text ironText;
    [SerializeField] private Text oreText;
    [SerializeField] private Text energyText;

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        PointSystem.Data(Action.Load, "food", ref food);
        PointSystem.Data(Action.Load, "iron", ref iron);
        PointSystem.Data(Action.Load, "ore", ref ore);
        PointSystem.Data(Action.Load, "energy", ref energy);

        UpdateTexts();
    }

    [ContextMenu("Save")]
    private void SaveData()
    {
        PointSystem.Data(Action.Save, "food", ref food);
        PointSystem.Data(Action.Save, "iron", ref iron);
        PointSystem.Data(Action.Save, "ore", ref ore);
        PointSystem.Data(Action.Save, "energy", ref energy);
    }

    public void Add<T>(ref T _var, T _amount)
    {
        PointSystem.Add(ref _var, _amount);

        SaveData();

        UpdateTexts();
    }

    private void UpdateTexts()
    {
        foodText.text = food.ToString();
        ironText.text = iron.ToString();
        oreText.text = ore.ToString();
        energyText.text = energy.ToString();
    }
}