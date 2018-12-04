using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitEnum UnitType;

    [SerializeField] private Material iron, food, ore;

    private Renderer meshRenderer;

    // Unit following
    public Transform followTrans;
    public bool follow;

    private void Start()
    {
        followTrans = FindObjectOfType<PlayerInteraction>().transform;

        meshRenderer = GetComponent<MeshRenderer>();

        ChangeMaterial();
    }

    private void ChangeMaterial()
    {
        var mats = meshRenderer.materials;

        switch (UnitType)
        {
            case UnitEnum.Ijzer:
                mats[0] = iron;
                break;

            case UnitEnum.Voedsel:
                mats[0] = food;
                break;

            case UnitEnum.Erts:
                mats[0] = ore;
                break;
        }

        meshRenderer.materials = mats;
    }

    private void FixedUpdate()
    {
        if (follow)
        {
            transform.position = new Vector3(followTrans.position.x, followTrans.position.y + 2f, followTrans.position.z);
        }
    }
}