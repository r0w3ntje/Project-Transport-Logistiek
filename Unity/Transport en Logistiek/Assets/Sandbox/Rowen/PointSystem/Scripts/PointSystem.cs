using System.Collections;
using System.Collections.Generic;
using Systems.Singleton;
using UnityEngine;

namespace Systems.PointSystem
{
    public enum Action
    {
        Save, Load
    }

    public class PointSystem<T> :MonoBehaviour
    {
        public static T Data(T _variable, Action _dataAction, string _playerprefs)
        {
            Debug.Log("Data");

            object var = _variable;

            if (typeof(T) == typeof(float))
            {
                if (_dataAction == Action.Save)
                {
                    PlayerPrefs.SetFloat(_playerprefs, (float)var);
                }
                else if (_dataAction == Action.Load)
                {
                    var = PlayerPrefs.GetFloat(_playerprefs, 0f);
                }
            }
            else if (typeof(T) == typeof(int))
            {
                if (_dataAction == Action.Save)
                {
                    PlayerPrefs.SetInt(_playerprefs, (int)var);
                }
                else if (_dataAction == Action.Load)
                {
                    var = PlayerPrefs.GetInt(_playerprefs, 0);
                }
            }
            else if (typeof(T) == typeof(string))
            {
                if (_dataAction == Action.Save)
                {
                    PlayerPrefs.SetString(_playerprefs, (string)var);
                }
                else if (_dataAction == Action.Load)
                {
                    var = PlayerPrefs.GetString(_playerprefs, "");
                }
            }
            else
            {
                Debug.LogError("WRONG TYPE: " + var);
            }

            return (T)var;
        }

        public static T Add(T _variable, T _amount)
        {
            object var = _variable;
            object amount = _amount;

            object points = var;

            if (typeof(T) == typeof(float))
            {
                points = (float)var + (float)amount;
            }
            else if (typeof(T) == typeof(int))
            {
                points = (int)var + (int)amount;
            }
            else
            {
                Debug.LogError("WRONG TYPE: " + var);
            }

            return (T)points;
        }
    }
}