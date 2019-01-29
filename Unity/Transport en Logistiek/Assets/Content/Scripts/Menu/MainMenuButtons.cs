using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("BlockOutMaan Rowen");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("BlockOutMaan Rowen");
    }

    public void StopGame()
    {
        Application.Quit();
    }
}
