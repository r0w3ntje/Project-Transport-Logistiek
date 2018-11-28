using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitEnum UnitType;

    [SerializeField] private Material iron, food;

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
            case UnitEnum.Iron:
                mats[0] = iron;
                break;

            case UnitEnum.Food:
                mats[0] = food;
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