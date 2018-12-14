using UnityEngine;

namespace Systems.PointSystem
{
    public enum Action
    {
        Save, Load, Reset
    }

    public class PointSystem
    {
        public static void Data<T>(Action _dataAction, string _playerpref, ref T _var)
        {
            object variable = _var;

            if (typeof(T) == typeof(float))
            {
                if (_dataAction == Action.Save) PlayerPrefs.SetFloat(_playerpref, (float)variable);
                else if (_dataAction == Action.Load) variable = PlayerPrefs.GetFloat(_playerpref, (float)variable);
                else if (_dataAction == Action.Reset)
                {
                    PlayerPrefs.SetFloat(_playerpref, (float)variable);
                    variable = PlayerPrefs.GetFloat(_playerpref);
                }
            }
            else if (typeof(T) == typeof(int))
            {
                if (_dataAction == Action.Save) PlayerPrefs.SetInt(_playerpref, (int)variable);
                else if (_dataAction == Action.Load) variable = PlayerPrefs.GetInt(_playerpref, (int)variable);
                else if (_dataAction == Action.Reset)
                {
                    PlayerPrefs.SetInt(_playerpref, (int)variable);
                    variable = PlayerPrefs.GetInt(_playerpref);
                }
            }
            else if (typeof(T) == typeof(string))
            {
                if (_dataAction == Action.Save) PlayerPrefs.SetString(_playerpref, (string)variable);
                else if (_dataAction == Action.Load) variable = PlayerPrefs.GetString(_playerpref, (string)variable);
                else if (_dataAction == Action.Reset)
                {
                    PlayerPrefs.SetString(_playerpref, (string)variable);
                    variable = PlayerPrefs.GetString(_playerpref);
                }
            }
            else
            {
                Debug.LogError("WRONG TYPE: " + variable.GetType());
            }

            _var = (T)variable;
        }

        public static void Add<T>(ref T _var, T _amount)
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

            _var = (T)points;
        }
    }
}