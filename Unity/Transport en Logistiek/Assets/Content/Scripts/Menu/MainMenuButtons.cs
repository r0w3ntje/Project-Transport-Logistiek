using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("BlockOutMaan");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("BlockOutMaan");
    }

    public void StopGame()
    {
        Application.Quit();
    }
}
