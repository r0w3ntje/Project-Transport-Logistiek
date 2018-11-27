using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitEnum UnitType;

    [SerializeField] private Material iron, food;

    private Renderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

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
}