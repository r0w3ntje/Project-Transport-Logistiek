using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    public GameObject playerCamera;

    [Header("Settings")]
    public float sensitivityX;
    public float sensitivityY;
    [Range(70, 90)]
    public int FOV;

    private float currentRotY;

    private void Start()
    {
        CursorLockToggle();

        Camera.main.fieldOfView = FOV;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CursorLockToggle();
        }

        if (Cursor.lockState != CursorLockMode.Locked) return;

        Rotation();
    }

    private void CursorLockToggle()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        Cursor.visible = Cursor.lockState == CursorLockMode.None;
    }

    private void Rotation()
    {
        //Rotate the player
        float x = Input.GetAxis("Mouse X") * sensitivityX;
        transform.Rotate(0, x, 0);

        //Rotate the camera
        float y = Input.GetAxis("Mouse Y") * sensitivityY;

        currentRotY += y;
        currentRotY = Mathf.Clamp(currentRotY, -90f, 90f); //Limit rotation

        playerCamera.transform.localEulerAngles = new Vector3(-currentRotY, 0f, 0f);
    }
}