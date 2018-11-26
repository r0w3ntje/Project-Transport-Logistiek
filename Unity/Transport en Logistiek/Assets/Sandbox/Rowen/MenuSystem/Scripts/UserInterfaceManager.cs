using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuEnum
{
    Main = 0,
    InGame = 1,
    Pause = 2,
}

public class UserInterfaceManager : MonoBehaviour
{

}

public static class MenuEvent
{
    public delegate void Menu(MenuEnum _menu);
    public static event Menu OnMenu;

    public static MenuEnum _currentMenu = MenuEnum.Main;

    public static void CallEvent(MenuEnum _menu)
    {
        _currentMenu = _menu;

        if (OnMenu != null)
            OnMenu(_menu);
    }
}
