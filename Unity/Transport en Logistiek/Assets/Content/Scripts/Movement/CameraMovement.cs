using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
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

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            cameraHolder.position += cameraHolder.forward * movementSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            cameraHolder.position += cameraHolder.right * movementSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        }

        private void Rotate()
        {
            if (Input.GetMouseButtonDown(1))
            {
                mouseOrigin = Input.mousePosition;
                isRotating = true;
            }

            if (Input.GetMouseButtonUp(1))
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