using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterfaceItem : MonoBehaviour
{
    [Header("User Interface Item")]
    [Tooltip("To what menu belongs this object?")]
    protected MenuEnum menu;

    private void Awake()
    {
        MenuEvent.OnMenu += Action;
    }

    private void Start()
    {
        Action(MenuEvent._currentMenu);
    }

    protected virtual void Update()
    {
        if (menu != MenuEvent._currentMenu) return;
    }

    protected void Action(MenuEnum _menu)
    {
        if (_menu != menu) return;
    }

    public void OpenMenu(MenuEnum _menu)
    {
        MenuEvent.CallEvent(_menu);
    }

    public void OpenScene(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}