using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject mainMenu, optiesMenu;

    public void StartGame()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("MainScene");
    }

    //public void OptiesMenu()
    //{
    //    mainMenu.SetActive(false);
    //    optiesMenu.SetActive(true);
    //    Debug.Log("Opties");
    //}

    public void StopGame()
    {
        Debug.Log("Stop");
        Application.Quit();
    }
}
