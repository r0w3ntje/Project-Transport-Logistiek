using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameToMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }
    }
}