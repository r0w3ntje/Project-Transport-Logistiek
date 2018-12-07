using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform cameraHolder;
        [SerializeField] private new Transform camera;

        [SerializeField] private float fixedHeight;

        [SerializeField] private float movementSpeed;
        [SerializeField] private float dragSpeed;

        private bool isRotating;

        private Vector3 mouseOrigin;
        private Vector3 oldMousePos;

        private Vector3 rotationPosition;
        private Vector3 otherRotation;

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

        private Vector3 pos;
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
                pos = Camera.main.ScreenToViewportPoint(mouseOrigin - Input.mousePosition);
                cameraHolder.eulerAngles = new Vector3(0f, pos.x * dragSpeed, 0f) + otherRotation;
            }
            else otherRotation = cameraHolder.eulerAngles;
        }
    }
}