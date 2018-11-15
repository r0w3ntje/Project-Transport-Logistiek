using UnityEngine;

namespace Systems.PointSystem
{
    public enum Action
    {
        Save, Load, Delete
    }

    public class PointSystem
    {
        public static T Data<T>(Action _dataAction, string _playerpref, T _var)
        {
            object variable = _var;

            if (typeof(T) == typeof(float))
            {
                if (_dataAction == Action.Save) PlayerPrefs.SetFloat(_playerpref, (float)variable);
                else if (_dataAction == Action.Load) variable = PlayerPrefs.GetFloat(_playerpref, 0f);
                else if (_dataAction == Action.Delete)
                {
                    PlayerPrefs.SetFloat(_playerpref, 0f);
                    variable = PlayerPrefs.GetFloat(_playerpref);
                }
            }
            else if (typeof(T) == typeof(int))
            {
                if (_dataAction == Action.Save) PlayerPrefs.SetInt(_playerpref, (int)variable);
                else if (_dataAction == Action.Load) variable = PlayerPrefs.GetInt(_playerpref, 0);
                else if (_dataAction == Action.Delete)
                {
                    PlayerPrefs.SetInt(_playerpref, 0);
                    variable = PlayerPrefs.GetInt(_playerpref);
                }
            }
            else if (typeof(T) == typeof(string))
            {
                if (_dataAction == Action.Save) PlayerPrefs.SetString(_playerpref, (string)variable);
                else if (_dataAction == Action.Load) variable = PlayerPrefs.GetString(_playerpref, "");
                else if (_dataAction == Action.Delete)
                {
                    PlayerPrefs.SetString(_playerpref, "");
                    variable = PlayerPrefs.GetString(_playerpref);
                }
            }
            else
            {
                Debug.LogError("WRONG TYPE: " + variable.GetType());
            }

            return (T)variable;
        }

        public static T Add<T>(T _var, T _amount)
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