using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : UserInterfaceItem
{
    [Header("Pause Menu")]
    [SerializeField] private bool isPaused;

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }
}