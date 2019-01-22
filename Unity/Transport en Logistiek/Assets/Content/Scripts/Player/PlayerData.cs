using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using Systems.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : Singleton<PlayerData>
{
    [Header("Variables")]
    public int iron;
    public int ore;
    public int helium;
    public float energy;
    public float maxEnergy;

    [Header("UI Elements")]
    [SerializeField] private Text ironText;
    [SerializeField] private Text oreText;
    [SerializeField] private Text heliumText;
    [SerializeField] private Slider energyBar;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        LoadData();

        Add(ref energy, 50);
    }

    private void LoadData()
    {
        PointSystem.Data(Action.Load, "iron", ref iron);
        PointSystem.Data(Action.Load, "ore", ref ore);
        PointSystem.Data(Action.Load, "helium", ref helium);
        PointSystem.Data(Action.Load, "energy", ref energy);

        UpdateTexts();
    }

    [ContextMenu("Save")]
    private void SaveData()
    {
        PointSystem.Data(Action.Save, "iron", ref iron);
        PointSystem.Data(Action.Save, "ore", ref ore);
        PointSystem.Data(Action.Save, "helium", ref helium);
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
        ironText.text = iron.ToString();
        oreText.text = ore.ToString();
        heliumText.text = helium.ToString();
        energyBar.value = energy / maxEnergy;
    }

    public bool HasSufficientUnits<T>(UnitEnum _unit, T _amount)
    {
        bool hasSufficientUnits = false;

        object variable = _amount;

        switch (_unit)
        {
            case UnitEnum.Ijzer:
                if (iron >= (int)variable)
                    hasSufficientUnits = true;
                break;
            case UnitEnum.Stroom:
                if (energy >= (float)variable)
                    hasSufficientUnits = true;
                break;
            case UnitEnum.Erts:
                if (ore >= (int)variable)
                    hasSufficientUnits = true;
                break;
            case UnitEnum.Helium:
                if (helium >= (int)variable)
                    hasSufficientUnits = true;
                break;
            default:
                hasSufficientUnits = false;
                break;
        }

        return hasSufficientUnits;
    }

    public float LoadFloat(string playerprefs)
    {
        return PlayerPrefs.GetFloat(playerprefs, 0f);
    }
    public void SaveFloat(string playerprefs, float value)
    {
        PlayerPrefs.SetFloat(playerprefs, value);
    }

    public int LoadInt(string playerprefs)
    {
        return PlayerPrefs.GetInt(playerprefs, 0);
    }
    public void SaveInt(string playerprefs, int value)
    {
        PlayerPrefs.SetInt(playerprefs, value);
    }

    public string LoadString(string playerprefs)
    {
        return PlayerPrefs.GetString(playerprefs, "");
    }
    public void SaveString(string playerprefs, string value)
    {
        PlayerPrefs.SetString(playerprefs, value);
    }
}