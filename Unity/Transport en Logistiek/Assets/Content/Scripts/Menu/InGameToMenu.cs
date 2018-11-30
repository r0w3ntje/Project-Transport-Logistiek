using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameToMenu : MonoBehaviour
{
    private void Update()
    {
#if UNITY_EDITOR
        return;
#endif

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("BlockOutMaan");
        }
    }
}