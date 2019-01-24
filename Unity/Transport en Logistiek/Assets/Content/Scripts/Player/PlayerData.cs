using System.Collections;
using System.Collections.Generic;
using Systems.PointSystem;
using Systems.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : Singleton<PlayerData>
{
    [Header("Variables")]
    public float maxEnergy;

    [Header("UI Elements")]
    [SerializeField] private Text ironText;
    [SerializeField] private Text oreText;
    [SerializeField] private Text heliumText;
    [SerializeField] private Slider energyBar;

    public Dictionary<UnitEnum, float> unitData = new Dictionary<UnitEnum, float>
    {
        { UnitEnum.Iron, 0f },
        { UnitEnum.Energy, 0f },
        { UnitEnum.Ore, 0f },
        { UnitEnum.Helium, 0f }
    };

    //private void Awake()
    //{
    //    PlayerPrefs.DeleteAll();
    //}

    private void Start()
    {
        //LoadData();

        Add(UnitEnum.Energy, 50f);
    }

    //private void LoadData()
    //{
    //    PointSystem.Data(Action.Load, "iron", ref iron);
    //    PointSystem.Data(Action.Load, "ore", ref ore);
    //    PointSystem.Data(Action.Load, "helium", ref helium);
    //    PointSystem.Data(Action.Load, "energy", ref energy);

    //    UpdateTexts();
    //}

    //[ContextMenu("Save")]
    //private void SaveData()
    //{
    //    PointSystem.Data(Action.Save, "iron", ref iron);
    //    PointSystem.Data(Action.Save, "ore", ref ore);
    //    PointSystem.Data(Action.Save, "helium", ref helium);
    //    PointSystem.Data(Action.Save, "energy", ref energy);
    //}

    public void Add(UnitEnum _unit, float _amount)
    {
        unitData[_unit] += _amount;

        //Debug.Log(_unit + ", " + unitData[_unit]);

        //PointSystem.Add(ref unitData[_unit], _amount);

        //SaveData();

        UpdateTexts();
    }

    private void UpdateTexts()
    {
        ironText.text = unitData[UnitEnum.Iron].ToString();
        oreText.text = unitData[UnitEnum.Ore].ToString();
        heliumText.text = unitData[UnitEnum.Helium].ToString();
        energyBar.value = unitData[UnitEnum.Energy] / maxEnergy;
    }

    public bool HasSufficientUnits(UnitEnum _unit, float _amount)
    {
        return unitData[_unit] >= _amount;
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