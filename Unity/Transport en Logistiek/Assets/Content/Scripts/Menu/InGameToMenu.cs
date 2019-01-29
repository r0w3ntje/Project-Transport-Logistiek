using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameToMenu : MonoBehaviour
{
    [Header("Audio")]
    [FMODUnity.EventRef]
    public string openMenu = "event:/Menu/Open";

    [FMODUnity.EventRef]
    public string music = "event:/Music";

    FMOD.Studio.EventInstance musicEv;

    private void Start()
    {
        musicEv = FMODUnity.RuntimeManager.CreateInstance(music);
       
        musicEv.start();
    }

    private void Update()
    {
        musicEv.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            musicEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            FMODUnity.RuntimeManager.PlayOneShot(openMenu, transform.position);
            SceneManager.LoadScene("Main");
        }
    }
}