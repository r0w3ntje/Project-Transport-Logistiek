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

    public class PointSystem<T> : MonoBehaviour
    {
        public static T Data(Action _dataAction, string _playerpref, T _var)
        {
            object var = _var;

            if (typeof(T) == typeof(float))
            {
                if (_dataAction == Action.Save) PlayerPrefs.SetFloat(_playerpref, (float)var);
                else if (_dataAction == Action.Load) var = PlayerPrefs.GetFloat(_playerpref, 0f);
            }
            else if (typeof(T) == typeof(int))
            {
                if (_dataAction == Action.Save) PlayerPrefs.SetInt(_playerpref, (int)var);
                else if (_dataAction == Action.Load) var = PlayerPrefs.GetInt(_playerpref, 0);
            }
            else if (typeof(T) == typeof(string))
            {
                if (_dataAction == Action.Save) PlayerPrefs.SetString(_playerpref, (string)var);
                else if (_dataAction == Action.Load) var = PlayerPrefs.GetString(_playerpref, "");
            }
            else
            {
                Debug.LogError("WRONG TYPE: " + var.GetType());
            }

            return (T)var;
        }

        public static T Add(T _var, T _amount)
        {
            object points = null;

            if (typeof(T) == typeof(float))
            {
                points = (float)(object)_var + (float)(object)_amount;
            }
            else if (typeof(T) == typeof(int))
            {
                points = (int)(object)_var + (int)(object)_amount;
            }
            else
            {
                Debug.LogError("WRONG TYPE");
            }

            return (T)points;
        }
    }
}