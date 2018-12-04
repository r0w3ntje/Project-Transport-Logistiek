using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    public GameObject playerCamera;

    [Header("Settings")]
    public float sensitivityX;
    public float sensitivityY;
    [Range(70, 90)]
    public int FOV;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.fieldOfView = FOV;
    }

    private void Update()
    {
        Rotation();
    }

    private float currentRotY;

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