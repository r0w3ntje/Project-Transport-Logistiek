using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TransportLogistiek
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform cameraHolder;
        [SerializeField] private new Transform camera;

        [SerializeField] private float movementSpeed;
        [SerializeField] private float dragSpeed;

        private bool isRotating;

        private Vector3 mouseOrigin;        
        private Vector3 currentRotation;

        private float zoom;
        [SerializeField] private float zoomSpeed;
        [SerializeField] Vector2 maxZoom = new Vector2(-50f, 0f);

        [Header("Audio")]
        [FMODUnity.EventRef]
        public string MouseClick = "event:/Menu/Click";

        private void Update()
        {
            Move();
            Rotate();
            Click();
        }

        private void Move()
        {
            cameraHolder.position += cameraHolder.forward * movementSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            cameraHolder.position += cameraHolder.right * movementSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;

            cameraHolder.position = new Vector3(
                Mathf.Clamp(cameraHolder.position.x, 180, 300),
                Mathf.Clamp(cameraHolder.position.y, 4, 30),
                Mathf.Clamp(cameraHolder.position.z, 210, 300));
        }

        private void Click()//just for audio
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                FMODUnity.RuntimeManager.PlayOneShot(MouseClick, transform.position);
                Debug.Log("dd");
            }
        }

        private void Rotate()
        {
            if (Input.GetMouseButtonDown(2))
            {
                mouseOrigin = Input.mousePosition;
                isRotating = true;
            }

            if (Input.GetMouseButtonUp(2))
            {
                isRotating = false;
            }

            if (isRotating)
            {
                var pos = Camera.main.ScreenToViewportPoint(mouseOrigin - Input.mousePosition);
                cameraHolder.eulerAngles = new Vector3(0f, pos.x * dragSpeed, 0f) + currentRotation;
            }
            else currentRotation = cameraHolder.eulerAngles;

            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                zoom += (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
                zoom = Mathf.Clamp(zoom, maxZoom.x, maxZoom.y);

                camera.transform.localPosition = new Vector3(0f, 0f, zoom);
            }
        }
    }
}