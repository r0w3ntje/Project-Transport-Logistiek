using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Header("Rotate")]
    [SerializeField] private Vector3 rotateDirection;
    [SerializeField] private float speed;

    private Transform thisTransform;
    //private RectTransform thisRectTransform;

    private void Start()
    {
        thisTransform = GetComponent<Transform>();
        if (thisTransform != null) return;

        //thisRectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        if (thisTransform != null) thisTransform.Rotate(rotateDirection * speed * Time.fixedDeltaTime);
        //if (thisRectTransform != null) thisRectTransform.Rotate(rotateDirection * speed * Time.fixedDeltaTime);
    }
}