using UnityEngine;

namespace TransportLogistiek
{
    public class Unit : MonoBehaviour
    {
        [Header("Unit")]
        public UnitEnum UnitType;

        [Header("Materials")]
        [SerializeField] private Material iron;
        [SerializeField] private Material food;
        [SerializeField] private Material ore;

        [Header("Components")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] Renderer meshRenderer;

        //private Vector3 offset;

        private CameraMovement camMove;

        private Vector3 unitOrigin, mouseOrigin;
        private Vector3 basePos, pos2;

        private void Start()
        {
            //camMove = FindObjectOfType<CameraMovement>();

            ChangeMaterial();
        }

        //private void OnMouseDown()
        //{
        //    PlayerUnitPickup.Instance().unit = this.gameObject;
        //    rb.isKinematic = true;

        //    //unitOrigin = transform.position;
        //    //Debug.Log("unitOrigin" + unitOrigin);
        //    //mouseOrigin = Camera.main.ViewportPointToRay(Input.mousePosition).origin;
        //    //Debug.Log("mouseOrigin" + mouseOrigin);

        //    //Debug.Log("Difference" + (unitOrigin - mouseOrigin));

        //    //Debug.Log("New mouse origin" + ((unitOrigin - mouseOrigin) + mouseOrigin));
        //    unitOrigin = transform.position;
        //    mouseOrigin = Camera.main.ViewportPointToRay(Input.mousePosition).origin;
        //    basePos = (unitOrigin - mouseOrigin) + mouseOrigin;
        //}

        //private void OnMouseDrag()
        //{

        //    //Debug.Log("unitOrigin" + unitOrigin);
        //    //Debug.Log("mouseOrigin" + mouseOrigin);

        //    //Debug.Log("Difference" + (unitOrigin - mouseOrigin));

        //    var mousePos = Camera.main.ViewportPointToRay(Input.mousePosition).origin;
        //    mousePos += basePos;
        //    Debug.Log(mousePos);

        //    transform.position = mousePos;

        //    //Debug.Log("New mouse origin" + unitPosition);

        //    //transform.position = unitPosition;

        //    //var mouseWorldPos = Camera.main.ViewportPointToRay(Input.mousePosition);
        //    //var mouseWorldPos = Camera.main.ViewportPointToRay(Input.mousePosition);

        //    //Debug.Log(mouseWorldPos);

        //    //Debug.Log("MOUSEWORLDPOS" + mouseWorldPos);

        //    //transform.position = new Vector3(mouseWorldPos.x, 10f, mouseWorldPos.y);
        //    //transform.position = mouseWorldPos;
        //    //Debug.Log(" -1- " + transform.position + " -2- " + camMove.transform.position + " -3- " + offset);

        //    //Debug.Log("Mouse position 5" + Camera.main.ViewportPointToRay(Input.mousePosition));


        //    //var x =

        //    //Debug.Log(Camera.main.ViewportToWorldPoint(Input.mousePosition - offset));

        //    //Debug.Log("X: " + transform.position.x + " , Y:" + transform.position.y + ", Z: " + transform.position.z);
        //}

        //private void OnMouseUp()
        //{
        //    PlayerUnitPickup.Instance().unit = null;
        //    rb.isKinematic = false;
        //}

        private void ChangeMaterial()
        {
            var mats = meshRenderer.materials;

            switch (UnitType)
            {
                case UnitEnum.Ijzer:
                    mats[0] = iron;
                    break;

                case UnitEnum.Stroom:
                    mats[0] = food;
                    break;

                case UnitEnum.Erts:
                    mats[0] = ore;
                    break;
            }

            meshRenderer.materials = mats;
        }
    }
}